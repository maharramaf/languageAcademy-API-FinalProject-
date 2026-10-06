using Domain.Entities;
using Repository.Data;
using Repository.Repositories.Interfaces;


namespace Repository.Repositories
{
    public class ContactMessageRepository : BaseRepository<ContactMessage>, IContactMessageRepository
    {
        public ContactMessageRepository(AppDbContext context) : base(context) { }

        public async Task AddAsync(ContactMessage message)
        {
            await _dbSet.AddAsync(message);
            await _dbContext.SaveChangesAsync();
        }
    }
}
