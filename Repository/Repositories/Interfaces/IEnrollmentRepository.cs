using Domain.Entities;

namespace Repository.Repositories.Interfaces
{
    public interface IEnrollmentRepository : IBaseRepository<Enrollment>
    {
        Task<bool> ExistsAsync(string studentId, int courseId);
        Task AddAsync(Enrollment enrollment);
        Task<IReadOnlyList<Enrollment>> GetByStudentAsync(string studentId);
        Task<IReadOnlyList<string>> GetTeacherIdsByStudentAsync(string studentId);
        Task<IReadOnlyList<string>> GetStudentIdsByTeacherAsync(string teacherId);
        Task<IReadOnlyList<Enrollment>> GetByTeacherAsync(string teacherId);
        Task<int> CountByStudentAsync(string studentId);
    }
}
