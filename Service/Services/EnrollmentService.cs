using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Repository.Repositories.Interfaces;
using Service.Helpers.DTOs.Courses;
using Service.Helpers.DTOs.Enrollments;
using Service.Services.Interfaces;

namespace Service.Services
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly IEnrollmentRepository _enrollmentRepo;
        private readonly ICourseRepository _courseRepo;
        private readonly ILessonProgressRepository _progressRepo;
        private readonly UserManager<AppUser> _userManager;

        public EnrollmentService(
            IEnrollmentRepository enrollmentRepo,
            ICourseRepository courseRepo,
            ILessonProgressRepository progressRepo,
            UserManager<AppUser> userManager)
        {
            _enrollmentRepo = enrollmentRepo;
            _courseRepo = courseRepo;
            _progressRepo = progressRepo;
            _userManager = userManager;
        }

        public async Task<EnrollmentResultDto> EnrollAsync(string studentId, EnrollmentCreateDto dto)
        {
            if (string.IsNullOrWhiteSpace(studentId))
                return Fail("Student was not found.");

            if (dto.CourseId <= 0)
                return Fail("Course was not found.");

            var course = await _courseRepo.GetByIdAsync(dto.CourseId);
            if (course is null)
                return Fail("Course was not found.");

            if (await _enrollmentRepo.ExistsAsync(studentId, course.Id))
                return Ok(course);

            var user = await _userManager.FindByIdAsync(studentId);
            if (user is null)
                return Fail("Student was not found.");

            if (user.Plan < course.Type)
                return Fail($"Your {user.Plan} plan cannot enroll in a {course.Type} course. Choose a higher plan first.");

            await _enrollmentRepo.AddAsync(new Enrollment
            {
                StudentId = studentId,
                CourseId = course.Id
            });

            return Ok(course);
        }

        public async Task<IReadOnlyList<CourseDto>> GetMineAsync(string studentId)
        {
            if (string.IsNullOrWhiteSpace(studentId))
                return Array.Empty<CourseDto>();

            var items = await _enrollmentRepo.GetByStudentAsync(studentId);
            return items
                .Where(m => m.Course is not null)
                .Select(m => MapCourse(m.Course))
                .ToList();
        }

        public Task<bool> IsEnrolledAsync(string studentId, int courseId)
        {
            if (string.IsNullOrWhiteSpace(studentId) || courseId <= 0)
                return Task.FromResult(false);

            return _enrollmentRepo.ExistsAsync(studentId, courseId);
        }

        public async Task<CourseDetailDto?> GetLearnAsync(string studentId, string slug)
        {
            if (string.IsNullOrWhiteSpace(studentId) || string.IsNullOrWhiteSpace(slug))
                return null;

            var course = await _courseRepo.GetBySlugAsync(slug.Trim());
            if (course is null)
                return null;

            if (!await _enrollmentRepo.ExistsAsync(studentId, course.Id))
                return null;

            var done = await _progressRepo.GetLessonIdsByStudentAsync(studentId);
            var teacher = string.IsNullOrWhiteSpace(course.TeacherId)
                ? null
                : await _userManager.FindByIdAsync(course.TeacherId);

            return new CourseDetailDto
            {
                Id = course.Id,
                Slug = course.Slug,
                Title = course.Title,
                Type = course.Type.ToString().ToLowerInvariant(),
                Level = course.Level,
                Duration = course.Duration,
                Price = course.Price,
                Image = course.Image,
                Summary = course.Summary,
                Overview = course.Overview,
                Video = course.Video,
                TeacherEmail = teacher?.Email,
                TeacherName = DisplayName(teacher),
                Modules = course.Modules.OrderBy(m => m.Order).Select(module => new CourseModuleDto
                {
                    Id = module.Id,
                    Title = module.Title,
                    Info = module.Info,
                    Order = module.Order,
                    Lessons = module.Lessons.OrderBy(x => x.Order).Select(lesson => new LessonDto
                    {
                        Id = lesson.Id,
                        Title = lesson.Title,
                        Kind = lesson.Kind.ToString().ToLowerInvariant(),
                        Video = lesson.Video,
                        Seconds = lesson.Seconds,
                        Order = lesson.Order,
                        Completed = done.Contains(lesson.Id)
                    }).ToList()
                }).ToList()
            };
        }

        public async Task<IReadOnlyList<ClassmateDto>> GetClassmatesAsync(int courseId)
        {
            if (courseId <= 0)
                return Array.Empty<ClassmateDto>();

            var items = await _enrollmentRepo.GetByCourseAsync(courseId);
            return items
                .Where(m => m.Student is not null)
                .Select(m => new ClassmateDto
                {
                    Name = DisplayName(m.Student) ?? m.Student.Email ?? "Student",
                    Email = m.Student.Email ?? string.Empty
                })
                .ToList();
        }

        private static string? DisplayName(AppUser? user)
        {
            if (user is null)
                return null;

            var name = (user.Name + " " + user.Surname).Trim();
            return string.IsNullOrEmpty(name) ? user.Email : name;
        }

        private static EnrollmentResultDto Ok(Course course)
        {
            return new EnrollmentResultDto
            {
                Succeeded = true,
                Slug = course.Slug,
                Title = course.Title
            };
        }

        private static EnrollmentResultDto Fail(string error)
        {
            return new EnrollmentResultDto
            {
                Errors = new[] { error }
            };
        }

        private static CourseDto MapCourse(Course course)
        {
            return new CourseDto
            {
                Id = course.Id,
                Slug = course.Slug,
                Title = course.Title,
                Type = course.Type.ToString().ToLowerInvariant(),
                Level = course.Level,
                Duration = course.Duration,
                Price = course.Price,
                Image = course.Image,
                Summary = course.Summary,
                LessonCount = course.Modules.Sum(x => x.Lessons.Count),
                Video = course.Video
            };
        }
    }
}
