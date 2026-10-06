using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Repository.Data
{
    public static class AboutSeeder
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            if (!await context.Abouts.AnyAsync(m => m.PageKey == "home"))
            {
                context.Abouts.Add(new About
                {
                    PageKey = "home",
                    Eyebrow = "About us",
                    Title = "About MF Language Academy",
                    ParagraphOne = "MF Language Academy helps adults and young professionals speak with confidence. Lessons are practical, classes stay small, and every course has a clear path from the first class to a certificate.",
                    ParagraphTwo = "Study on campus in Boston or join live online groups with the same teachers, materials, and progress checks.",
                    FeatureOne = "Experienced Teachers",
                    FeatureTwo = "Modern Learning",
                    FeatureThree = "Flexible Courses",
                    FeatureFour = "International Environment",
                    ButtonText = "More About Us",
                    Image = "images/about.jpg",
                    ImageAlt = "Teacher guiding a small language class",
                    YearsStat = "10+",
                    YearsLabelLineOne = "Years of",
                    YearsLabelLineTwo = "language teaching"
                });
            }

            if (!await context.Abouts.AnyAsync(m => m.PageKey == "about"))
            {
                context.Abouts.Add(new About
                {
                    PageKey = "about",
                    Eyebrow = "Who we are",
                    Title = "Small classes. Clear progress.",
                    ParagraphOne = "Students join MF Language Academy to pass an exam, join a new team, or feel at home in another country. We keep groups small, publish the weekly plan, and measure progress with speaking checks instead of surprise tests.",
                    ParagraphTwo = "Campus classes meet at Language Plaza. Online classes use the same teachers and the same materials, so a student can switch format when travel or work changes.",
                    FeatureOne = "Experienced Teachers",
                    FeatureTwo = "Modern Learning",
                    FeatureThree = "Flexible Courses",
                    FeatureFour = "International Environment",
                    ButtonText = "",
                    Image = "images/about.jpg",
                    ImageAlt = "A language class in session",
                    YearsStat = "10+",
                    YearsLabelLineOne = "Years teaching",
                    YearsLabelLineTwo = "in Boston"
                });
            }

            if (context.ChangeTracker.HasChanges())
                await context.SaveChangesAsync();
        }
    }
}
