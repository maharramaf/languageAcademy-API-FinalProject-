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

        public async Task AddAsync(Lesson lesson)
        {
            await _dbSet.AddAsync(lesson);
            await _dbContext.SaveChangesAsync();
        }
    }
}
