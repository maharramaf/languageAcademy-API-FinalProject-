using Domain.Entities;


namespace Repository.Repositories.Interfaces
{
    public interface IReviewSectionRepository : IBaseRepository<ReviewSection>
    {
        Task<ReviewSection?> GetLatestAsync();
    }
}
