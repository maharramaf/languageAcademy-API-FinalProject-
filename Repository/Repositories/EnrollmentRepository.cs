using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories
{
    public class EnrollmentRepository : BaseRepository<Enrollment>, IEnrollmentRepository
    {
        public EnrollmentRepository(AppDbContext context) : base(context) { }

        public Task<bool> ExistsAsync(string studentId, int courseId)
        {
            return _dbSet.AnyAsync(m => m.StudentId == studentId && m.CourseId == courseId);
        }

        public async Task AddAsync(Enrollment enrollment)
        {
            await _dbSet.AddAsync(enrollment);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<Enrollment>> GetByStudentAsync(string studentId)
        {
            return await _dbSet.AsNoTracking()
                .Include(m => m.Course)
                    .ThenInclude(m => m.Modules)
                        .ThenInclude(m => m.Lessons)
                .Where(m => m.StudentId == studentId)
                .OrderByDescending(m => m.CreatedAt)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<string>> GetTeacherIdsByStudentAsync(string studentId)
        {
            return await _dbSet.AsNoTracking()
                .Where(m => m.StudentId == studentId && m.Course.TeacherId != null)
                .Select(m => m.Course.TeacherId!)
                .Distinct()
                .ToListAsync();
        }

        public async Task<IReadOnlyList<string>> GetStudentIdsByTeacherAsync(string teacherId)
        {
            return await _dbSet.AsNoTracking()
                .Where(m => m.Course.TeacherId == teacherId)
                .Select(m => m.StudentId)
                .Distinct()
                .ToListAsync();
        }
    }
}
