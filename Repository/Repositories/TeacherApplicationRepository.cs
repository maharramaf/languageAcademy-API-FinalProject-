using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories
{
    public class TeacherApplicationRepository : BaseRepository<TeacherApplication>, ITeacherApplicationRepository
    {
        public TeacherApplicationRepository(AppDbContext context) : base(context) { }

        public async Task AddAsync(TeacherApplication application)
        {
            await _dbSet.AddAsync(application);
            await _dbContext.SaveChangesAsync();
        }

        public Task<TeacherApplication?> GetByIdAsync(int id)
        {
            return _dbSet.FirstOrDefaultAsync(m => m.Id == id);
        }

        public Task SaveAsync()
        {
            return _dbContext.SaveChangesAsync();
        }

        public Task<bool> HasPendingEmailAsync(string email)
        {
            return _dbSet.AnyAsync(m =>
                m.Email == email && m.Status == TeacherApplicationStatus.Pending);
        }
    }
}
