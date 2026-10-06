using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Repository.Data
{
    public static class HeroSeeder
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            if (await context.Heroes.AnyAsync()) return;

            context.Heroes.Add(new Hero
            {
                Subtitle = "Modern language school",
                Title = "Learn Languages. Open New Doors.",
                Text = "Improve your language skills with professional teachers and modern learning methods.",
                Button1 = "Explore Courses",
                Button2 = "Get Started",
                Point1 = "Small classes",
                Point2 = "Certified teachers",
                Point3 = "Online and on campus",
                Image = "images/hero.jpg",
                ImageAlt = "Students collaborating during a language lesson",
                Students = "1,000+",
                StudentsText = "Active students",
                Rating = "4.9/5",
                RatingText = "Average rating"
            });

            await context.SaveChangesAsync();
        }
    }
}
