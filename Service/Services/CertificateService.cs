using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Repository.Repositories.Interfaces;
using Service.Helpers.DTOs.Certificates;
using Service.Services.Interfaces;

namespace Service.Services
{
    public class CertificateService : ICertificateService
    {
        private readonly ICertificateRepository _certificateRepo;
        private readonly IEnrollmentRepository _enrollmentRepo;
        private readonly ICourseRepository _courseRepo;
        private readonly ILessonProgressRepository _progressRepo;
        private readonly UserManager<AppUser> _userManager;

        public CertificateService(
            ICertificateRepository certificateRepo,
            IEnrollmentRepository enrollmentRepo,
            ICourseRepository courseRepo,
            ILessonProgressRepository progressRepo,
            UserManager<AppUser> userManager)
        {
            _certificateRepo = certificateRepo;
            _enrollmentRepo = enrollmentRepo;
            _courseRepo = courseRepo;
            _progressRepo = progressRepo;
            _userManager = userManager;
        }

        public async Task<IReadOnlyList<CertificateDto>> GetMineAsync(string studentId)
        {
            if (string.IsNullOrWhiteSpace(studentId))
                return Array.Empty<CertificateDto>();

            var enrollments = await _enrollmentRepo.GetByStudentAsync(studentId);
            foreach (var enrollment in enrollments)
                await TryIssueAsync(studentId, enrollment.CourseId);

            var items = await _certificateRepo.GetByStudentAsync(studentId);
            var result = new List<CertificateDto>();
            foreach (var item in items)
                result.Add(await MapAsync(item));
            return result;
        }

        public async Task<CertificateDto?> GetBySlugAsync(string studentId, string slug)
        {
            if (string.IsNullOrWhiteSpace(studentId) || string.IsNullOrWhiteSpace(slug))
                return null;

            var course = await _courseRepo.GetBySlugAsync(slug.Trim());
            if (course is not null)
                await TryIssueAsync(studentId, course.Id);

            var certificate = await _certificateRepo.GetByStudentAndSlugAsync(studentId, slug.Trim());
            return certificate is null ? null : await MapAsync(certificate);
        }

        public async Task<CertificateDto?> TryIssueAsync(string studentId, int courseId)
        {
            if (string.IsNullOrWhiteSpace(studentId) || courseId <= 0)
                return null;

            if (!await _enrollmentRepo.ExistsAsync(studentId, courseId))
                return null;

            var existing = await _certificateRepo.GetByStudentAndCourseAsync(studentId, courseId);
            if (existing is not null)
                return await MapAsync(existing);

            var course = await _courseRepo.GetByIdWithLessonsAsync(courseId);
            if (course is null)
                return null;

            var lessonIds = course.Modules.SelectMany(m => m.Lessons).Select(m => m.Id).ToList();
            if (lessonIds.Count == 0)
                return null;

            var done = await _progressRepo.GetLessonIdsByStudentAsync(studentId);
            if (lessonIds.Any(id => !done.Contains(id)))
                return null;

            if (await _certificateRepo.ExistsAsync(studentId, courseId))
            {
                var again = await _certificateRepo.GetByStudentAndCourseAsync(studentId, courseId);
                return again is null ? null : await MapAsync(again);
            }

            var slug = (course.Slug ?? "course").ToUpperInvariant().Replace("-", "");
            var certificate = new Certificate
            {
                StudentId = studentId,
                CourseId = courseId,
                Number = $"MF-{slug}-{Guid.NewGuid():N}".ToUpperInvariant()
            };
            await _certificateRepo.AddAsync(certificate);

            var saved = await _certificateRepo.GetByStudentAndCourseAsync(studentId, courseId);
            var dto = saved is null ? null : await MapAsync(saved);
            if (dto is not null)
                dto.Created = true;
            return dto;
        }

        private async Task<CertificateDto> MapAsync(Certificate certificate)
        {
            var enrollment = await _enrollmentRepo.GetAsync(certificate.StudentId, certificate.CourseId);
            return new CertificateDto
            {
                Number = certificate.Number,
                CourseTitle = certificate.Course?.Title ?? string.Empty,
                CourseSlug = certificate.Course?.Slug ?? string.Empty,
                StudentName = DisplayName(certificate.Student),
                TeacherName = DisplayName(certificate.Course?.Teacher),
                StartedAt = enrollment?.CreatedAt ?? certificate.CreatedAt,
                IssuedAt = certificate.CreatedAt
            };
        }

        private static string DisplayName(AppUser? user)
        {
            if (user is null)
                return string.Empty;

            var name = (user.Name + " " + user.Surname).Trim();
            return string.IsNullOrEmpty(name) ? user.Email ?? string.Empty : name;
        }
    }
}
