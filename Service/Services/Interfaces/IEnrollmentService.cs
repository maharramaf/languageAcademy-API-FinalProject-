using Service.Helpers.DTOs.Courses;
using Service.Helpers.DTOs.Enrollments;

namespace Service.Services.Interfaces
{
    public interface IEnrollmentService
    {
        Task<EnrollmentResultDto> EnrollAsync(string studentId, EnrollmentCreateDto dto);
        Task<IReadOnlyList<CourseDto>> GetMineAsync(string studentId);
        Task<bool> IsEnrolledAsync(string studentId, int courseId);
        Task<CourseDetailDto?> GetLearnAsync(string studentId, string slug);
    }
}
