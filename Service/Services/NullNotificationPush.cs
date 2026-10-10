using Service.Helpers.DTOs.Notifications;
using Service.Services.Interfaces;

namespace Service.Services
{
    public class NullNotificationPush : INotificationPush
    {
        public Task PushAsync(string userId, NotificationDto item) => Task.CompletedTask;
    }
}
