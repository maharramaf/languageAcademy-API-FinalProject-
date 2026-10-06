using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repositories.Interfaces;


namespace Repository.Repositories
{
    public class StudentSectionRepository : BaseRepository<StudentSection>, IStudentSectionRepository
    {
        public StudentSectionRepository(AppDbContext context) : base(context) { }

        public async Task<StudentSection?> GetLatestAsync()
        {
            return await _dbSet.AsNoTracking()
                .OrderByDescending(m => m.CreatedAt)
                .FirstOrDefaultAsync();
        }
    }
}
