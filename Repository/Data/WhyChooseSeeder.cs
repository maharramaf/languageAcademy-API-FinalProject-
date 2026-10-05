using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Repository.Data
{
    public static class WhyChooseSeeder
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            if (await context.WhyChooses.AnyAsync()) return;

            context.WhyChooses.Add(new WhyChoose
            {
                Eyebrow = "Why choose us",
                Title = "A school built around speaking",
                Lead = "The method is simple: small groups, useful materials, and teachers who know how adults actually learn.",
                Cards = new List<WhyChooseCard>
                {
                    Card(1, "bi bi-person-workspace", "Expert Teachers", "Certified instructors with classroom experience and clear, kind feedback."),
                    Card(2, "bi bi-calendar2-week", "Flexible Schedule", "Morning, evening, and weekend groups so study fits around work."),
                    Card(3, "bi bi-laptop", "Online Learning", "Live online classes use the same plan as the Boston campus."),
                    Card(4, "bi bi-award", "Certificate", "Finish the course and receive a MF Language Academy certificate of completion."),
                    Card(5, "bi bi-people", "Small Classes", "Groups of 8 to 12 students so there is time to speak every lesson."),
                    Card(6, "bi bi-journal-richtext", "Modern Materials", "Current topics, audio, and tasks designed for adult learners.")
                }
            });

            await context.SaveChangesAsync();
        }

        private static WhyChooseCard Card(int sortOrder, string icon, string title, string text)
        {
            return new WhyChooseCard
            {
                SortOrder = sortOrder,
                Icon = icon,
                Title = title,
                Text = text
            };
        }
    }
}
