using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Repository.Data
{
    public static class AboutHeroSeeder
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            if (await context.AboutHeroes.AnyAsync()) return;

            context.AboutHeroes.Add(new AboutHero
            {
                Subtitle = "Our story",
                Title = "About MF Language Academy",
                Text = "MF Language Academy started as an evening English circle and grew into a full language school for adults who need results, not endless textbooks."
            });

            await context.SaveChangesAsync();
        }
    }
}
