using Domain.Entities;

namespace Repository.Repositories.Interfaces
{
    public interface ITeacherApplicationRepository : IBaseRepository<TeacherApplication>
    {
        Task AddAsync(TeacherApplication application);
        Task<TeacherApplication?> GetByIdAsync(int id);
        Task SaveAsync();
        Task<bool> HasPendingEmailAsync(string email);
    }
}
