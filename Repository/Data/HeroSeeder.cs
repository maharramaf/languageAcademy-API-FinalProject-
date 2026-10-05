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
                Eyebrow = "Modern language school",
                Title = "Learn Languages. Open New Doors.",
                Lead = "Improve your language skills with professional teachers and modern learning methods.",
                PrimaryButtonText = "Explore Courses",
                SecondaryButtonText = "Get Started",
                PointOne = "Small classes",
                PointTwo = "Certified teachers",
                PointThree = "Online and on campus",
                Image = "images/hero.jpg",
                ImageAlt = "Students collaborating during a language lesson",
                StudentsStat = "1,000+",
                StudentsLabel = "Active students",
                RatingStat = "4.9/5",
                RatingLabel = "Average rating"
            });

            await context.SaveChangesAsync();
        }
    }
}
