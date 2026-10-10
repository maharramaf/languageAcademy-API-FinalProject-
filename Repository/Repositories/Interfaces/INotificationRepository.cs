using Domain.Entities;

namespace Repository.Repositories.Interfaces
{
    public interface INotificationRepository
    {
        Task<IReadOnlyList<Notification>> GetByUserAsync(string userId);
        Task<Notification?> GetByIdAsync(int id);
        Task AddAsync(Notification notification);
        Task SaveAsync();
        Task MarkAllReadAsync(string userId);
    }
}
