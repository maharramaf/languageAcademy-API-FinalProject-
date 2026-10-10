using Domain.Entities;

namespace Repository.Repositories.Interfaces
{
    public interface ICertificateRepository
    {
        Task<bool> ExistsAsync(string studentId, int courseId);
        Task<Certificate?> GetByStudentAndCourseAsync(string studentId, int courseId);
        Task<Certificate?> GetByStudentAndSlugAsync(string studentId, string slug);
        Task<IReadOnlyList<Certificate>> GetByStudentAsync(string studentId);
        Task AddAsync(Certificate certificate);
    }
}
