using Domain.Entities;


namespace Repository.Repositories.Interfaces
{
    public interface ISubscriberRepository : IBaseRepository<Subscriber>
    {
        Task<Subscriber?> GetByEmailAsync(string email);
        Task AddAsync(Subscriber subscriber);
        Task SaveAsync();
    }
}
