using Service.Helpers.DTOs.Notifications;

namespace Service.Services.Interfaces
{
    public interface INotificationService
    {
        Task<IReadOnlyList<NotificationDto>> GetMineAsync(string userId);
        Task<bool> MarkReadAsync(string userId, int id);
        Task MarkAllReadAsync(string userId);
        Task<NotificationDto?> AddAsync(NotificationCreateDto dto);
    }
}
