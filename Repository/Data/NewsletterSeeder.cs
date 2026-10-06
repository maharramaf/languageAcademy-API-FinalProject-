using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Repository.Data
{
    public static class NewsletterSeeder
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            if (await context.NewsletterSections.AnyAsync()) return;

            context.NewsletterSections.Add(new NewsletterSection
            {
                Subtitle = "Newsletter",
                Title = "Course news, in your inbox",
                Text = "New groups, exam dates, and study tips from MF Language Academy."
            });

            await context.SaveChangesAsync();
        }
    }
}
