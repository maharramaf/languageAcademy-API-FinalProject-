using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repositories.Interfaces;


namespace Repository.Repositories
{
    public class NewsletterSectionRepository : BaseRepository<NewsletterSection>, INewsletterSectionRepository
    {
        public NewsletterSectionRepository(AppDbContext context) : base(context) { }

        public async Task<NewsletterSection?> GetLatestAsync()
        {
            return await _dbSet.AsNoTracking()
                .OrderByDescending(m => m.CreatedAt)
                .FirstOrDefaultAsync();
        }
    }
}
