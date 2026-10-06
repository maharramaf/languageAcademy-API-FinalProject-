using Domain.Entities;


namespace Repository.Repositories.Interfaces
{
    public interface IContactMessageRepository : IBaseRepository<ContactMessage>
    {
        Task AddAsync(ContactMessage message);
    }
}
