using Domain.Entities;
using Repository.Data;
using Repository.Repositories.Interfaces;


namespace Repository.Repositories
{
    public class StatItemRepository : BaseRepository<StatItem>, IStatItemRepository
    {
        public StatItemRepository(AppDbContext context) : base(context) { }
    }
}
