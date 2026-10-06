using Domain.Entities;


namespace Repository.Repositories.Interfaces
{
    public interface IStatsRepository : IBaseRepository<Stats>
    {
        Task<Stats?> GetWithItemsAsync();
    }
}
