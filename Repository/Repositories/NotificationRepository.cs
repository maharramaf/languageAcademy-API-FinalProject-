using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly AppDbContext _dbContext;

        public NotificationRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IReadOnlyList<Notification>> GetByUserAsync(string userId)
        {
            return await _dbContext.Notifications.AsNoTracking()
                .Where(m => m.UserId == userId)
                .OrderByDescending(m => m.CreatedAt)
                .Take(50)
                .ToListAsync();
        }

        public Task<Notification?> GetByIdAsync(int id)
        {
            return _dbContext.Notifications.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task AddAsync(Notification notification)
        {
            await _dbContext.Notifications.AddAsync(notification);
            await _dbContext.SaveChangesAsync();
        }

        public Task SaveAsync()
        {
            return _dbContext.SaveChangesAsync();
        }

        public async Task MarkAllReadAsync(string userId)
        {
            var items = await _dbContext.Notifications
                .Where(m => m.UserId == userId && !m.IsRead)
                .ToListAsync();
            if (items.Count == 0)
                return;

            foreach (var item in items)
                item.IsRead = true;

            await _dbContext.SaveChangesAsync();
        }
    }
}
