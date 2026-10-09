using Domain.Entities;
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

        public EnrollmentService(IEnrollmentRepository enrollmentRepo, ICourseRepository courseRepo)
        {
            _enrollmentRepo = enrollmentRepo;
            _courseRepo = courseRepo;
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
