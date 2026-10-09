using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class CourseRepository : BaseRepository<Course>, ICourseRepository
    {
        public CourseRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Course>> GetAllWithModulesAsync()
        {
            return await _dbSet.AsNoTracking()
                .Include(m => m.Modules)
                    .ThenInclude(m => m.Lessons)
                .OrderByDescending(m => m.CreatedAt)
                .ToListAsync();
        }

        public async Task<Course?> GetBySlugAsync(string slug)
        {
            return await _dbSet.AsNoTracking()
                .Include(m => m.Outcomes)
                .Include(m => m.Modules)
                    .ThenInclude(m => m.Lessons)
                .Include(m => m.Reviews)
                .FirstOrDefaultAsync(m => m.Slug == slug);
        }

        public Task<int> CountAsync()
        {
            return _dbSet.CountAsync();
        }

        public Task<bool> SlugExistsAsync(string slug, int? exceptId = null)
        {
            return exceptId is null
                ? _dbSet.AnyAsync(m => m.Slug == slug)
                : _dbSet.AnyAsync(m => m.Slug == slug && m.Id != exceptId);
        }

        public async Task AddAsync(Course course)
        {
            await _dbSet.AddAsync(course);
            await _dbContext.SaveChangesAsync();
        }

        public Task<Course?> GetByIdAsync(int id)
        {
            return _dbSet.FirstOrDefaultAsync(m => m.Id == id);
        }

        public Task SaveAsync()
        {
            return _dbContext.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var course = await _dbSet
                .Include(m => m.Reviews)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course is null)
                return false;

            if (course.Reviews.Count > 0)
                _dbContext.Set<Review>().RemoveRange(course.Reviews);

            _dbSet.Remove(course);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
