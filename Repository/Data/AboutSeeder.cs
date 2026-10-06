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
                    Subtitle = "About us",
                    Title = "About MF Language Academy",
                    Text1 = "MF Language Academy helps adults and young professionals speak with confidence. Lessons are practical, classes stay small, and every course has a clear path from the first class to a certificate.",
                    Text2 = "Study on campus in Boston or join live online groups with the same teachers, materials, and progress checks.",
                    Feature1 = "Experienced Teachers",
                    Feature2 = "Modern Learning",
                    Feature3 = "Flexible Courses",
                    Feature4 = "International Environment",
                    Button = "More About Us",
                    Image = "images/about.jpg",
                    ImageAlt = "Teacher guiding a small language class",
                    Years = "10+",
                    YearsText1 = "Years of",
                    YearsText2 = "language teaching"
                });
            }

            if (!await context.Abouts.AnyAsync(m => m.PageKey == "about"))
            {
                context.Abouts.Add(new About
                {
                    PageKey = "about",
                    Subtitle = "Who we are",
                    Title = "Small classes. Clear progress.",
                    Text1 = "Students join MF Language Academy to pass an exam, join a new team, or feel at home in another country. We keep groups small, publish the weekly plan, and measure progress with speaking checks instead of surprise tests.",
                    Text2 = "Campus classes meet at Language Plaza. Online classes use the same teachers and the same materials, so a student can switch format when travel or work changes.",
                    Feature1 = "Experienced Teachers",
                    Feature2 = "Modern Learning",
                    Feature3 = "Flexible Courses",
                    Feature4 = "International Environment",
                    Button = "",
                    Image = "images/about.jpg",
                    ImageAlt = "A language class in session",
                    Years = "10+",
                    YearsText1 = "Years teaching",
                    YearsText2 = "in Boston"
                });
            }

            if (context.ChangeTracker.HasChanges())
                await context.SaveChangesAsync();
        }
    }
}
