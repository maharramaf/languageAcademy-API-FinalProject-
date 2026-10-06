using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Repository.Data
{
    public static class StatsSeeder
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            if (await context.Stats.AnyAsync()) return;

            context.Stats.Add(new Stats
            {
                Years = 10
            });

            await context.SaveChangesAsync();
        }
    }
}
