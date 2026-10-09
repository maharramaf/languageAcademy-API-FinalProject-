using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories
{
    public class CourseModuleRepository : BaseRepository<CourseModule>, ICourseModuleRepository
    {
        public CourseModuleRepository(AppDbContext context) : base(context) { }

        public Task<CourseModule?> GetByIdAsync(int id)
        {
            return _dbSet.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<int> NextOrderAsync(int courseId)
        {
            var max = await _dbSet.Where(m => m.CourseId == courseId).MaxAsync(m => (int?)m.Order);
            return (max ?? 0) + 1;
        }

        public async Task AddAsync(CourseModule module)
        {
            await _dbSet.AddAsync(module);
            await _dbContext.SaveChangesAsync();
        }

        public Task SaveAsync()
        {
            return _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(CourseModule module)
        {
            _dbSet.Remove(module);
            await _dbContext.SaveChangesAsync();
        }
    }
}
