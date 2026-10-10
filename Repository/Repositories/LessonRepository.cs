using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories
{
    public class LessonRepository : BaseRepository<Lesson>, ILessonRepository
    {
        public LessonRepository(AppDbContext context) : base(context) { }

        public async Task<int> NextOrderAsync(int moduleId)
        {
            var max = await _dbSet.Where(m => m.CourseModuleId == moduleId).MaxAsync(m => (int?)m.Order);
            return (max ?? 0) + 1;
        }

        public Task<Lesson?> GetByIdAsync(int id)
        {
            return _dbSet.FirstOrDefaultAsync(m => m.Id == id);
        }

        public Task<Lesson?> GetWithCourseAsync(int id)
        {
            return _dbSet.AsNoTracking()
                .Include(m => m.CourseModule)
                    .ThenInclude(m => m.Course)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task AddAsync(Lesson lesson)
        {
            await _dbSet.AddAsync(lesson);
            await _dbContext.SaveChangesAsync();
        }

        public Task SaveAsync()
        {
            return _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Lesson lesson)
        {
            _dbSet.Remove(lesson);
            await _dbContext.SaveChangesAsync();
        }
    }
}
