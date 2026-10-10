using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories
{
    public class LessonProgressRepository : ILessonProgressRepository
    {
        private readonly AppDbContext _db;

        public LessonProgressRepository(AppDbContext db)
        {
            _db = db;
        }

        public Task<bool> ExistsAsync(string studentId, int lessonId)
        {
            return _db.LessonProgresses.AnyAsync(m => m.StudentId == studentId && m.LessonId == lessonId);
        }

        public async Task AddAsync(LessonProgress progress)
        {
            await _db.LessonProgresses.AddAsync(progress);
            await _db.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<int>> GetLessonIdsByStudentAsync(string studentId)
        {
            return await _db.LessonProgresses.AsNoTracking()
                .Where(m => m.StudentId == studentId)
                .Select(m => m.LessonId)
                .ToListAsync();
        }
    }
}
