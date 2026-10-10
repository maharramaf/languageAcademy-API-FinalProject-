using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Repository.Repositories.Interfaces;
using Service.Helpers.DTOs.Plans;
using Service.Services.Interfaces;

namespace Service.Services
{
    public class TeacherPlanService : ITeacherPlanService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ICourseRepository _courseRepo;

        public TeacherPlanService(UserManager<AppUser> userManager, ICourseRepository courseRepo)
        {
            _userManager = userManager;
            _courseRepo = courseRepo;
        }

        public async Task<PlanPageDto?> GetMineAsync(string userId)
        {
            var user = await FindUserAsync(userId);
            if (user is null) return null;

            var items = Catalog();
            var current = user.TeacherPlan.ToString().ToLowerInvariant();
            return new PlanPageDto
            {
                Current = current,
                CurrentTitle = items.FirstOrDefault(item => item.Type == current)?.Title ?? "Free Teacher",
                AssignedCount = await _courseRepo.CountByTeacherIdAsync(user.Id),
                CourseLimit = LimitOf(user.TeacherPlan),
                ShowUsage = true,
                Items = items
            };
        }

        public async Task<PlanResultDto> ChooseAsync(string userId, PlanChooseDto dto)
        {
            if (!Enum.TryParse<CourseType>(dto.Type?.Trim(), true, out var type)
                || !Enum.IsDefined(type))
                return Fail("Teacher plan must be Demo, Standard, or Premium.");

            var user = await FindUserAsync(userId);
            if (user is null)
                return Fail("Account was not found.");

            user.TeacherPlan = type;
            var updated = await _userManager.UpdateAsync(user);
            if (!updated.Succeeded)
                return Fail(updated.Errors.Select(e => e.Description));

            return new PlanResultDto
            {
                Succeeded = true,
                Current = user.TeacherPlan.ToString().ToLowerInvariant()
            };
        }

        private async Task<AppUser?> FindUserAsync(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                return null;

            return await _userManager.FindByIdAsync(userId);
        }

        private static List<PlanDto> Catalog()
        {
            return new List<PlanDto>
            {
                new()
                {
                    Type = "demo",
                    Title = "Free Teacher",
                    Price = 0,
                    Info = "Publish one assigned course. Demo billing only.",
                    CourseLimit = 1,
                    Features = new List<string>
                    {
                        "1 assigned course",
                        "Lesson studio and classroom",
                        "Classmates and messages",
                        "Demo billing only"
                    }
                },
                new()
                {
                    Type = "standard",
                    Title = "Standard Teacher",
                    Price = 19,
                    Info = "Assign up to 5 courses. No card charge in this test.",
                    CourseLimit = 5,
                    Features = new List<string>
                    {
                        "Up to 5 assigned courses",
                        "Lesson studio and classroom",
                        "Classmates and messages",
                        "Earnings preview",
                        "No card charge in this test"
                    }
                },
                new()
                {
                    Type = "premium",
                    Title = "Premium Teacher",
                    Price = 39,
                    Info = "Unlimited assigned courses. No card charge in this test.",
                    Features = new List<string>
                    {
                        "Unlimited assigned courses",
                        "Lesson studio and classroom",
                        "Classmates and messages",
                        "Earnings preview",
                        "No card charge in this test"
                    }
                }
            };
        }

        private static int? LimitOf(CourseType type)
        {
            return type switch
            {
                CourseType.Demo => 1,
                CourseType.Standard => 5,
                CourseType.Premium => null,
                _ => 1
            };
        }

        private static PlanResultDto Fail(string error) => Fail(new[] { error });

        private static PlanResultDto Fail(IEnumerable<string> errors)
        {
            return new PlanResultDto
            {
                Succeeded = false,
                Errors = errors.ToList()
            };
        }
    }
}
