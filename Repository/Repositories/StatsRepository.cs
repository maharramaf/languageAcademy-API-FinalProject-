using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repositories.Interfaces;


namespace Repository.Repositories
{
    public class StatsRepository : BaseRepository<Stats>, IStatsRepository
    {
        public StatsRepository(AppDbContext context) : base(context) { }

        public async Task<Stats?> GetLatestAsync()
        {
            return await _dbSet.AsNoTracking()
                .OrderByDescending(m => m.CreatedAt)
                .FirstOrDefaultAsync();
        }
    }
}
