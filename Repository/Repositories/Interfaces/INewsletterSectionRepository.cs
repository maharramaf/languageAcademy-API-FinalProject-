using Domain.Entities;


namespace Repository.Repositories.Interfaces
{
    public interface INewsletterSectionRepository : IBaseRepository<NewsletterSection>
    {
        Task<NewsletterSection?> GetLatestAsync();
    }
}
