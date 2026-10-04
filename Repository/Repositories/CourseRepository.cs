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
                .FirstOrDefaultAsync(m => m.Slug == slug);
        }
    }
}
