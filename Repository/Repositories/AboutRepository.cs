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
    public class AboutRepository : BaseRepository<About>, IAboutRepository
    {
        public AboutRepository(AppDbContext context) : base(context) { }

        public async Task<About?> GetByPageKeyAsync(string pageKey)
        {
            return await _dbSet.AsNoTracking()
                .FirstOrDefaultAsync(m => m.PageKey == pageKey);
        }
    }
}
