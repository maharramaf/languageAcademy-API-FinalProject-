using Domain.Entities;


namespace Repository.Repositories.Interfaces
{
    public interface IReviewRepository : IBaseRepository<Review>
    {
        Task<IReadOnlyList<Review>> GetHomeAsync();
        Task<IReadOnlyList<Review>> GetApprovedAsync();
    }
}
