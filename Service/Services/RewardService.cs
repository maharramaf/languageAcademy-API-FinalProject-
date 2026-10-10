using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Repository.Repositories.Interfaces;
using Service.Helpers.DTOs.Rewards;
using Service.Services.Interfaces;

namespace Service.Services
{
    public class RewardService : IRewardService
    {
        private const int CourseXp = 200;
        private const int CoursePoints = 100;

        private static readonly (int Level, int Xp, string Name)[] Levels =
        {
            (1, 0, "Beginner"),
            (2, 200, "Learner"),
            (3, 500, "Active Student"),
            (4, 1000, "Language Explorer"),
            (5, 2000, "Language Master")
        };

        private readonly UserManager<AppUser> _userManager;
        private readonly IEnrollmentRepository _enrollmentRepo;
        private readonly ILessonRepository _lessonRepo;
        private readonly ILessonProgressRepository _progressRepo;
        private readonly ICertificateService _certificateService;

        public RewardService(
            UserManager<AppUser> userManager,
            IEnrollmentRepository enrollmentRepo,
            ILessonRepository lessonRepo,
            ILessonProgressRepository progressRepo,
            ICertificateService certificateService)
        {
            _userManager = userManager;
            _enrollmentRepo = enrollmentRepo;
            _lessonRepo = lessonRepo;
            _progressRepo = progressRepo;
            _certificateService = certificateService;
        }

        public async Task<RewardsDto?> GetMineAsync(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                return null;

            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
                return null;

            if (BumpStreak(user))
                await _userManager.UpdateAsync(user);

            var courses = await _enrollmentRepo.CountByStudentAsync(userId);
            var xp = user.RewardXp + courses * CourseXp;
            var points = user.RewardPoints + courses * CoursePoints;
            var level = LevelFromXp(xp);
            var next = NextLevel(xp);
            var start = level.Xp;
            var end = next?.Xp ?? level.Xp;
            var span = Math.Max(1, end - start);
            var percent = next is null ? 100 : Math.Min(100, (xp - start) * 100 / span);

            return new RewardsDto
            {
                Xp = xp,
                Points = points,
                Level = level.Level,
                LevelName = level.Name,
                ProgressPercent = percent,
                NextLevelText = next is null
                    ? "Max level reached"
                    : $"Progress to next level: {percent}%",
                StreakDays = user.RewardStreakDays,
                LessonsDone = user.RewardLessons,
                HomeworkDone = user.RewardHomework,
                QuizzesDone = user.RewardQuizzes,
                CoursesDone = courses,
                Achievements = new List<RewardAchievementDto>
                {
                    Badge("homework_hero", "Homework Hero", user.RewardHomework, 5),
                    Badge("quiz_master", "Quiz Master", user.RewardQuizzes, 10),
                    Badge("streak_7", "7 Day Streak", user.RewardStreakDays, 7),
                    Badge("course_champion", "Course Champion", courses, 1),
                    Badge("fast_learner", "Fast Learner", user.RewardLessons, 10)
                }
            };
        }

        public async Task<RewardCompleteResultDto> CompleteLessonAsync(string userId, int lessonId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                return Fail("Account was not found.");

            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
                return Fail("Account was not found.");

            var lesson = await _lessonRepo.GetWithCourseAsync(lessonId);
            if (lesson?.CourseModule?.Course is null)
                return Fail("Lesson was not found.");

            if (!await _enrollmentRepo.ExistsAsync(userId, lesson.CourseModule.CourseId))
                return Fail("Enroll in this course first.");

            var kind = lesson.Kind.ToString().ToLowerInvariant();
            if (await _progressRepo.ExistsAsync(userId, lessonId))
                return new RewardCompleteResultDto { Succeeded = true, AlreadyCompleted = true, Kind = kind };

            var (xp, points) = GainFor(lesson.Kind);
            user.RewardXp += xp;
            user.RewardPoints += points;
            if (lesson.Kind == LessonKind.Quiz)
                user.RewardQuizzes += 1;
            else if (lesson.Kind == LessonKind.Homework)
                user.RewardHomework += 1;
            else
                user.RewardLessons += 1;

            BumpStreak(user);
            var updated = await _userManager.UpdateAsync(user);
            if (!updated.Succeeded)
                return Fail(updated.Errors.Select(e => e.Description));

            await _progressRepo.AddAsync(new LessonProgress
            {
                StudentId = userId,
                LessonId = lessonId
            });

            var certificate = await _certificateService.TryIssueAsync(userId, lesson.CourseModule.CourseId);

            return new RewardCompleteResultDto
            {
                Succeeded = true,
                XpGained = xp,
                PointsGained = points,
                Kind = kind,
                CertificateReady = certificate?.Created == true,
                CourseSlug = certificate?.CourseSlug ?? lesson.CourseModule.Course.Slug
            };
        }

        private static (int Xp, int Points) GainFor(LessonKind kind)
        {
            return kind switch
            {
                LessonKind.Quiz => (50, 30),
                LessonKind.Homework => (50, 20),
                _ => (20, 0)
            };
        }

        private static RewardCompleteResultDto Fail(string error) => Fail(new[] { error });

        private static RewardCompleteResultDto Fail(IEnumerable<string> errors)
        {
            return new RewardCompleteResultDto
            {
                Succeeded = false,
                Errors = errors.ToList()
            };
        }

        private static bool BumpStreak(AppUser user)
        {
            var today = DateTime.UtcNow.Date;
            var last = user.RewardLastActive?.Date;
            if (last == today)
                return false;

            user.RewardStreakDays = last == today.AddDays(-1)
                ? user.RewardStreakDays + 1
                : 1;
            user.RewardLastActive = today;
            return true;
        }

        private static (int Level, int Xp, string Name) LevelFromXp(int xp)
        {
            var current = Levels[0];
            foreach (var item in Levels)
            {
                if (xp >= item.Xp)
                    current = item;
            }

            return current;
        }

        private static (int Level, int Xp, string Name)? NextLevel(int xp)
        {
            foreach (var item in Levels)
            {
                if (xp < item.Xp)
                    return item;
            }

            return null;
        }

        private static RewardAchievementDto Badge(string key, string title, int done, int total)
        {
            return new RewardAchievementDto
            {
                Key = key,
                Title = title,
                Done = Math.Min(done, total),
                Total = total,
                Unlocked = done >= total
            };
        }
    }
}
