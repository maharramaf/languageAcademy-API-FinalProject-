using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repositories.Interfaces;


namespace Repository.Repositories
{
    public class TeacherSectionRepository : BaseRepository<TeacherSection>, ITeacherSectionRepository
    {
        public TeacherSectionRepository(AppDbContext context) : base(context) { }

        public async Task<TeacherSection?> GetWithTeachersAsync()
        {
            return await _dbSet.AsNoTracking()
                .Include(m => m.Teachers)
                .OrderByDescending(m => m.CreatedAt)
                .FirstOrDefaultAsync();
        }
    }
}
