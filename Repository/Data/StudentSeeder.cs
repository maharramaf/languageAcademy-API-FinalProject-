using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Repository.Data
{
    public static class StudentSeeder
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            if (!await context.StudentSections.AnyAsync())
            {
                context.StudentSections.Add(new StudentSection
                {
                    Subtitle = "Student life",
                    Title = "Learners with a reason to speak",
                    Text = "From exam candidates to new arrivals, students come for a schedule they can keep and a classroom where they are heard.",
                    StoriesSubtitle = "Stories",
                    StoriesTitle = "Recent student highlights",
                    StoriesText = "Approved stories from students who finished a recent group."
                });
            }

            var results = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                ["Amira Hassan"] = "IELTS band 7.5",
                ["Luca Bianchi"] = "Completed in 8 weeks",
                ["Priya Shah"] = "Beginner certificate",
                ["Noah Keller"] = "A1 speaking check",
                ["Leila Rahman"] = "Fluency workshop"
            };

            var reviews = await context.Reviews.ToListAsync();
            foreach (var review in reviews)
            {
                if (!string.IsNullOrWhiteSpace(review.Result)) continue;
                if (results.TryGetValue(review.Name, out var result))
                    review.Result = result;
            }

            if (!reviews.Any(m => m.Name == "Leila Rahman"))
            {
                var intermediate = await context.Courses.FirstOrDefaultAsync(m => m.Slug == "english-intermediate");
                if (intermediate is not null)
                {
                    context.Reviews.Add(new Review
                    {
                        CourseId = intermediate.Id,
                        Order = 5,
                        Rating = 5,
                        Name = "Leila Rahman",
                        Text = "I understood podcasts but froze in groups. The workshop gave me longer turns and better questions.",
                        Photo = "images/student-leila.jpg",
                        PhotoAlt = "Portrait of Leila Rahman",
                        Result = "Fluency workshop",
                        Home = false,
                        Approved = true
                    });
                }
            }

            await context.SaveChangesAsync();
        }
    }
}
