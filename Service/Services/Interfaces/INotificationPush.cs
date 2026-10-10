using Service.Helpers.DTOs.Notifications;

namespace Service.Services.Interfaces
{
    public interface INotificationPush
    {
        Task PushAsync(string userId, NotificationDto item);
    }
}
