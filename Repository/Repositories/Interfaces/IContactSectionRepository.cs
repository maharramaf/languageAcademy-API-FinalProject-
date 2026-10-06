using Domain.Entities;


namespace Repository.Repositories.Interfaces
{
    public interface IContactSectionRepository : IBaseRepository<ContactSection>
    {
        Task<ContactSection?> GetLatestAsync();
    }
}
