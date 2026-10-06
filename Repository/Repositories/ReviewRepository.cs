using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repositories.Interfaces;


namespace Repository.Repositories
{
    public class ReviewRepository : BaseRepository<Review>, IReviewRepository
    {
        public ReviewRepository(AppDbContext context) : base(context) { }

        public async Task<IReadOnlyList<Review>> GetHomeAsync()
        {
            return await _dbSet.AsNoTracking()
                .Include(m => m.Course)
                .Where(m => m.Approved && m.Home)
                .OrderBy(m => m.Order)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Review>> GetApprovedAsync()
        {
            return await _dbSet.AsNoTracking()
                .Include(m => m.Course)
                .Where(m => m.Approved)
                .OrderBy(m => m.Order)
                .ThenByDescending(m => m.CreatedAt)
                .ToListAsync();
        }
    }
}
