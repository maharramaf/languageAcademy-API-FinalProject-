using Domain.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Repository.Data
{
    public static class ContactSeeder
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            const string map = "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3039.3357604239964!2d49.850050932510364!3d40.37925051168906!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x40307d079efb5163%3A0xc20aa51a5f0b5e01!2sCode%20Academy!5e0!3m2!1saz!2saz!4v1790675942336!5m2!1saz!2saz";

            if (await context.ContactSections.AnyAsync())
            {
                var rows = await context.ContactSections.ToListAsync();
                foreach (var row in rows)
                {
                    var current = MapEmbed.Normalize(row.Map);
                    if (string.IsNullOrEmpty(current))
                        row.Map = map;
                    else if (row.Map != current)
                        row.Map = current;
                }

                await context.SaveChangesAsync();
                return;
            }

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
                Saturday = "Sat 10:00–14:00",
                Map = map
            });

            await context.SaveChangesAsync();
        }
    }
}
