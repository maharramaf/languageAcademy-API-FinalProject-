using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Repository.Data
{
    public static class HowWeWorkSeeder
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            if (await context.HowWeWorks.AnyAsync()) return;

            context.HowWeWorks.Add(new HowWeWork
            {
                Subtitle = "How we work",
                Title = "Mission, classrooms, and care",
                Cards = new List<HowWeWorkCard>
                {
                    Card(1, "bi bi-bullseye", "Mission", "Help adults speak clearly enough to study, work, and travel without translating every sentence."),
                    Card(2, "bi bi-buildings", "Campus and online", "Bright rooms in Boston and live online groups that follow the same weekly rhythm."),
                    Card(3, "bi bi-heart", "A calm classroom", "Teachers correct kindly, students speak often, and nobody is left to figure out homework alone.")
                }
            });

            await context.SaveChangesAsync();
        }

        private static HowWeWorkCard Card(int sortOrder, string icon, string title, string text)
        {
            return new HowWeWorkCard
            {
                Order = sortOrder,
                Icon = icon,
                Title = title,
                Text = text
            };
        }
    }
}
