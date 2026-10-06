using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repositories.Interfaces;


namespace Repository.Repositories
{
    public class StatsRepository : BaseRepository<Stats>, IStatsRepository
    {
        public StatsRepository(AppDbContext context) : base(context) { }

        public async Task<Stats?> GetWithItemsAsync()
        {
            return await _dbSet.AsNoTracking()
                .Include(m => m.Items)
                .OrderByDescending(m => m.CreatedAt)
                .FirstOrDefaultAsync();
        }
    }
}
