using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repositories.Interfaces;


namespace Repository.Repositories
{
    public class SubscriberRepository : BaseRepository<Subscriber>, ISubscriberRepository
    {
        public SubscriberRepository(AppDbContext context) : base(context) { }

        public async Task<Subscriber?> GetByEmailAsync(string email)
        {
            return await _dbSet.FirstOrDefaultAsync(m => m.Email == email);
        }

        public async Task AddAsync(Subscriber subscriber)
        {
            await _dbSet.AddAsync(subscriber);
            await _dbContext.SaveChangesAsync();
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
