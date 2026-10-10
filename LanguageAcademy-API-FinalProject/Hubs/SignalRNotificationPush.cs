using Microsoft.AspNetCore.SignalR;
using Service.Helpers.DTOs.Notifications;
using Service.Services.Interfaces;

namespace LanguageAcademy_API_FinalProject.Hubs
{
    public class SignalRNotificationPush : INotificationPush
    {
        private readonly IHubContext<NotificationHub> _hub;

        public SignalRNotificationPush(IHubContext<NotificationHub> hub)
        {
            _hub = hub;
        }

        public Task PushAsync(string userId, NotificationDto item)
        {
            if (string.IsNullOrWhiteSpace(userId) || item is null)
                return Task.CompletedTask;

            return _hub.Clients.User(userId).SendAsync("ReceiveNotification", item);
        }
    }
}
