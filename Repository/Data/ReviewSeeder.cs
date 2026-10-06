using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Repository.Data
{
    public static class ReviewSeeder
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            if (await context.ReviewSections.AnyAsync()) return;

            var ielts = await context.Courses.FirstOrDefaultAsync(m => m.Slug == "ielts");
            var business = await context.Courses.FirstOrDefaultAsync(m => m.Slug == "business-english");
            var spanish = await context.Courses.FirstOrDefaultAsync(m => m.Slug == "spanish");
            var german = await context.Courses.FirstOrDefaultAsync(m => m.Slug == "german");
            if (ielts is null || business is null || spanish is null || german is null) return;

            context.ReviewSections.Add(new ReviewSection
            {
                Subtitle = "Testimonials",
                Title = "What students say after class",
                Text = "Real goals: a visa interview, a new team, a first conversation abroad."
            });

            context.Reviews.AddRange(
                Item(ielts.Id, 1, 5, "Amira Hassan",
                    "The IELTS speaking labs were the first place I stopped memorizing answers. I left with a 7.5 and a calmer exam day.",
                    "images/student-amira.jpg", "Portrait of Amira Hassan", "IELTS band 7.5"),
                Item(business.Id, 2, 5, "Luca Bianchi",
                    "Business English gave me phrases I use in stand-ups every morning. The group was small enough to actually practice.",
                    "images/student-luca.jpg", "Portrait of Luca Bianchi", "Completed in 8 weeks"),
                Item(spanish.Id, 3, 4, "Priya Shah",
                    "I wanted Spanish for a family trip. By week six I could order, ask for directions, and understand the replies.",
                    "images/student-priya.jpg", "Portrait of Priya Shah", "Beginner certificate"),
                Item(german.Id, 4, 5, "Noah Keller",
                    "German finally clicked because Markus taught grammar inside real dialogues. I use it at the bakery near my office.",
                    "images/student-noah.jpg", "Portrait of Noah Keller", "A1 speaking check")
            );

            await context.SaveChangesAsync();
        }

        private static Review Item(int courseId, int order, int rating, string name, string text, string photo, string photoAlt, string result)
        {
            return new Review
            {
                CourseId = courseId,
                Order = order,
                Rating = rating,
                Name = name,
                Text = text,
                Photo = photo,
                PhotoAlt = photoAlt,
                Result = result,
                Home = true,
                Approved = true
            };
        }
    }
}
