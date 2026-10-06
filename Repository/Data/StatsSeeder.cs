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
                Items = new List<StatItem>
                {
                    Item(1, "students", "bi bi-people", "Students", "+", 0),
                    Item(2, "courses", "bi bi-journal-bookmark", "Courses", "+", 0),
                    Item(3, "teachers", "bi bi-person-badge", "Teachers", "+", 0),
                    Item(4, "years", "bi bi-mortarboard", "Years Experience", "+", 10)
                }
            });

            await context.SaveChangesAsync();
        }

        private static StatItem Item(int sortOrder, string sourceKey, string icon, string label, string suffix, int storedValue)
        {
            return new StatItem
            {
                SortOrder = sortOrder,
                SourceKey = sourceKey,
                Icon = icon,
                Label = label,
                Suffix = suffix,
                StoredValue = storedValue
            };
        }
    }
}
