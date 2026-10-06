using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repositories.Interfaces;


namespace Repository.Repositories
{
    public class ReviewSectionRepository : BaseRepository<ReviewSection>, IReviewSectionRepository
    {
        public ReviewSectionRepository(AppDbContext context) : base(context) { }

        public async Task<ReviewSection?> GetLatestAsync()
        {
            return await _dbSet.AsNoTracking()
                .OrderByDescending(m => m.CreatedAt)
                .FirstOrDefaultAsync();
        }
    }
}
