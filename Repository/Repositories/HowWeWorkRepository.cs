using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class HowWeWorkRepository : BaseRepository<HowWeWork>, IHowWeWorkRepository
    {
        public HowWeWorkRepository(AppDbContext context) : base(context) { }

        public async Task<HowWeWork?> GetWithCardsAsync()
        {
            return await _dbSet.AsNoTracking()
                .Include(m => m.Cards)
                .OrderByDescending(m => m.CreatedAt)
                .FirstOrDefaultAsync();
        }
    }
}
