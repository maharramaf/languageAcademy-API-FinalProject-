using Domain.Entities;
using Repository.Repositories.Interfaces;
using Service.Helpers.DTOs.Notifications;
using Service.Services.Interfaces;

namespace Service.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepo;
        private readonly INotificationPush _push;

        public NotificationService(INotificationRepository notificationRepo, INotificationPush push)
        {
            _notificationRepo = notificationRepo;
            _push = push;
        }

        public async Task<IReadOnlyList<NotificationDto>> GetMineAsync(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                return Array.Empty<NotificationDto>();

            var items = await _notificationRepo.GetByUserAsync(userId);
            return items.Select(Map).ToList();
        }

        public async Task<bool> MarkReadAsync(string userId, int id)
        {
            if (string.IsNullOrWhiteSpace(userId) || id <= 0)
                return false;

            var item = await _notificationRepo.GetByIdAsync(id);
            if (item is null || item.UserId != userId)
                return false;

            if (!item.IsRead)
            {
                item.IsRead = true;
                await _notificationRepo.SaveAsync();
            }

            return true;
        }

        public Task MarkAllReadAsync(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                return Task.CompletedTask;

            return _notificationRepo.MarkAllReadAsync(userId);
        }

        public async Task<NotificationDto?> AddAsync(NotificationCreateDto dto)
        {
            var userId = (dto.UserId ?? string.Empty).Trim();
            if (string.IsNullOrWhiteSpace(userId))
                return null;

            var item = new Notification
            {
                UserId = userId,
                Type = Limit(dto.Type, 40, "announcement"),
                Title = Limit(dto.Title, 160, "Notification"),
                Body = Limit(dto.Body, 400, string.Empty),
                Href = Limit(dto.Href, 240, string.Empty)
            };
            await _notificationRepo.AddAsync(item);

            var mapped = Map(item);
            await _push.PushAsync(userId, mapped);
            return mapped;
        }

        private static NotificationDto Map(Notification item)
        {
            return new NotificationDto
            {
                Id = item.Id,
                Type = item.Type,
                Title = item.Title,
                Body = item.Body,
                Href = item.Href,
                Read = item.IsRead,
                CreatedAt = DateTime.SpecifyKind(item.CreatedAt, DateTimeKind.Utc)
            };
        }

        private static string Limit(string? value, int max, string fallback)
        {
            var text = (value ?? string.Empty).Trim();
            if (text.Length == 0)
                return fallback;
            return text.Length <= max ? text : text[..max];
        }
    }
}
