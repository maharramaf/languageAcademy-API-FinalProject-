using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Repository.Data
{
    public static class TeacherSeeder
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            if (await context.TeacherSections.AnyAsync()) return;

            context.TeacherSections.Add(new TeacherSection
            {
                Subtitle = "Teachers",
                Title = "Learn with people who teach for a living",
                Text = "Our faculty lead courses, write materials, and stay in the classroom.",
                Teachers = new List<Teacher>
                {
                    CreateTeacher(1, "Elena Marquez", "English Program Lead",
                        "Designs the beginner pathway and teaches the Monday speaking lab.",
                        "Elena has taught English for twelve years in Boston and Madrid. She keeps classes practical, with speaking time in every lesson and feedback students can use the same week.",
                        "images/teacher-elena.jpg", "Portrait of Elena Marquez", "bi bi-instagram"),
                    CreateTeacher(2, "Daniel Okonkwo", "IELTS Instructor",
                        "Coaches writing and speaking with calm, timed exam practice.",
                        "Daniel prepares candidates for Academic IELTS. His feedback follows the public band descriptors, so students always know why a score moved.",
                        "images/teacher-daniel.jpg", "Portrait of Daniel Okonkwo", "bi bi-twitter-x"),
                    CreateTeacher(3, "Sophie Laurent", "Spanish Instructor",
                        "Brings culture, travel dialogues, and clear grammar into class.",
                        "Sophie teaches beginner Spanish with a balance of conversation and structure. Students practice both Latin American and Peninsular varieties.",
                        "images/teacher-sophie.jpg", "Portrait of Sophie Laurent", "bi bi-instagram"),
                    CreateTeacher(4, "Markus Weber", "German Instructor",
                        "Makes cases and pronunciation feel manageable from week one.",
                        "Markus focuses on the German beginners need for transport, housing, and introductions. Grammar is taught inside short dialogues, not long lectures.",
                        "images/teacher-markus.jpg", "Portrait of Markus Weber", "bi bi-facebook")
                }
            });

            await context.SaveChangesAsync();
        }

        private static Teacher CreateTeacher(
            int order,
            string name,
            string role,
            string summary,
            string bio,
            string photo,
            string photoAlt,
            string socialIcon)
        {
            return new Teacher
            {
                Order = order,
                Name = name,
                Role = role,
                Summary = summary,
                Bio = bio,
                Photo = photo,
                PhotoAlt = photoAlt,
                LinkedIn = "#",
                SocialIcon = socialIcon,
                Social = "#"
            };
        }
    }
}
