using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Repository.Data
{
    public static class AboutSeeder
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            if (await context.Abouts.AnyAsync()) return;

            context.Abouts.Add(new About
            {
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

            await context.SaveChangesAsync();
        }
    }
}
