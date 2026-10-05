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
    public class WhyChooseRepository : BaseRepository<WhyChoose>, IWhyChooseRepository
    {
        public WhyChooseRepository(AppDbContext context) : base(context) { }

        public async Task<WhyChoose?> GetWithCardsAsync()
        {
            return await _dbSet.AsNoTracking()
                .Include(m => m.Cards)
                .OrderByDescending(m => m.CreatedAt)
                .FirstOrDefaultAsync();
        }
    }
}
