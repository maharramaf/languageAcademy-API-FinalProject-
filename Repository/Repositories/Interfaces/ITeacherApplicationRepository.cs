using Domain.Entities;

namespace Repository.Repositories.Interfaces
{
    public interface ITeacherApplicationRepository : IBaseRepository<TeacherApplication>
    {
        Task AddAsync(TeacherApplication application);
        Task<bool> HasPendingEmailAsync(string email);
    }
}
