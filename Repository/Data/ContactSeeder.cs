using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Repository.Data
{
    public static class ContactSeeder
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            if (await context.ContactSections.AnyAsync()) return;

            context.ContactSections.Add(new ContactSection
            {
                Subtitle = "Admissions",
                Title = "Contact the language school",
                Text = "Ask about a level, a schedule, or a visit.",
                Address = "250 Language Plaza, 4th Floor",
                City = "Boston, MA 02110",
                Phone = "+1 (555) 014-2280",
                Email = "hello@mflanguage.academy",
                Hours = "Mon–Fri 9:00–18:00",
                Saturday = "Sat 10:00–14:00"
            });

            await context.SaveChangesAsync();
        }
    }
}
