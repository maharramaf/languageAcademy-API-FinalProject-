using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repositories.Interfaces;


namespace Repository.Repositories
{
    public class ContactSectionRepository : BaseRepository<ContactSection>, IContactSectionRepository
    {
        public ContactSectionRepository(AppDbContext context) : base(context) { }

        public async Task<ContactSection?> GetLatestAsync()
        {
            return await _dbSet.AsNoTracking()
                .OrderByDescending(m => m.CreatedAt)
                .FirstOrDefaultAsync();
        }
    }
}
