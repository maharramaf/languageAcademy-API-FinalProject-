using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Repository.Data
{
    public static class CourseSeeder
    {
        private static string Embed(string youtubeId)
        {
            return "https://www.youtube-nocookie.com/embed/" + youtubeId + "?rel=0&modestbranding=1";
        }

        public static async Task SeedAsync(AppDbContext context)
        {
            if (await context.Courses.AnyAsync()) return;

            context.Courses.AddRange(
                BuildEnglishBeginner(),
                BuildEnglishIntermediate(),
                BuildIelts(),
                BuildBusinessEnglish(),
                BuildGerman(),
                BuildSpanish(),
                BuildFrench(),
                BuildConversation()
            );

            await context.SaveChangesAsync();
        }

        private static Course BuildEnglishBeginner()
        {
            return new Course
            {
                Slug = "english-beginner",
                Title = "English A1 — Complete Beginner Course",
                Type = CourseType.Demo,
                Level = "Beginner",
                Duration = "8 weeks",
                Price = 0,
                Image = "images/course-beginner.jpg",
                Summary = "Start English from zero with guided speaking, everyday vocabulary, and clear grammar you can use the same day.",
                Overview = "This beginner pathway takes you from first greetings to short everyday conversations. Each module mixes video, reading, and a short quiz.",
                Video = Embed("v1ZWFsz0V5U"),
                Outcomes = Outcomes(
                    "Introduce yourself in English",
                    "Understand basic conversations",
                    "Build everyday vocabulary",
                    "Use basic grammar",
                    "Improve pronunciation",
                    "Speak in common daily situations"
                ),
                Modules = new List<CourseModule>
                {
                    Module(1, "Module 01 — Introduction to English", "4 lessons · 45 min",
                        Lesson(1, "Lesson 01 — Greetings", LessonKind.Video, Embed("I_tRSrPru94"), 360),
                        Lesson(2, "Lesson 02 — Introducing Yourself", LessonKind.Text, null, 480),
                        Lesson(3, "Lesson 03 — Basic Vocabulary", LessonKind.Video, Embed("o5ghhSAxopw"), 361),
                        Lesson(4, "Quiz — Introduction check", LessonKind.Quiz, null, 600)
                    ),
                    Module(2, "Module 02 — Everyday English", "5 lessons · 58 min",
                        Lesson(1, "Lesson 01 — Daily Routines", LessonKind.Video, Embed("QBi0U99zo3Q"), 505),
                        Lesson(2, "Lesson 02 — Present Simple", LessonKind.Text, null, 660),
                        Lesson(3, "Lesson 03 — Asking Questions", LessonKind.Video, Embed("gCs4knrlnD4"), 420),
                        Lesson(4, "Practice sheet", LessonKind.Download, null, 0),
                        Lesson(5, "Quiz — Everyday English", LessonKind.Quiz, null, 600)
                    )
                }
            };
        }

        private static Course BuildEnglishIntermediate()
        {
            return new Course
            {
                Slug = "english-intermediate",
                Title = "English Intermediate",
                Type = CourseType.Premium,
                Level = "Intermediate",
                Duration = "10 weeks",
                Price = 119,
                Image = "images/course-intermediate.jpg",
                Summary = "Move from careful sentences to fluent discussion. You will practice opinion, story, and workplace English.",
                Overview = "Learners at this level already know the basics. The course pushes accuracy and flow with debates, short writing, and real listening.",
                Video = Embed("h9HvFZUgxiM"),
                Outcomes = Outcomes(
                    "Hold a five-minute opinion conversation",
                    "Use linking language in stories and emails",
                    "Understand common fast speech",
                    "Give and receive clear feedback"
                ),
                Modules = new List<CourseModule>
                {
                    Module(1, "Module 01 — Fluency habits", "3 lessons · 40 min",
                        Lesson(1, "Lesson 01 — Small talk", LessonKind.Video, Embed("h9HvFZUgxiM"), 900),
                        Lesson(2, "Lesson 02 — Speaking clearly", LessonKind.Video, Embed("eIho2S0ZahI"), 598),
                        Lesson(3, "Lesson 03 — Study smarter", LessonKind.Video, Embed("IlU-zDU6aQ0"), 1500)
                    )
                }
            };
        }

        private static Course BuildIelts()
        {
            return new Course
            {
                Slug = "ielts",
                Title = "IELTS Preparation",
                Type = CourseType.Premium,
                Level = "Upper intermediate",
                Duration = "8 weeks",
                Price = 119,
                Image = "images/course-ielts.jpg",
                Summary = "Train for the Academic IELTS with timed tasks, score-focused feedback, and strategies for each paper.",
                Overview = "You will practice Listening, Reading, Writing, and Speaking every week.",
                Video = Embed("2SI0twnNEN8"),
                Outcomes = Outcomes(
                    "Plan Task 1 and Task 2 under time pressure",
                    "Build speaking answers for all three parts",
                    "Avoid the errors that cost bands",
                    "Sit two full mock tests"
                ),
                Modules = new List<CourseModule>
                {
                    Module(1, "Module 01 — Speaking criteria", "3 lessons · 35 min",
                        Lesson(1, "Lesson 01 — Fluency & coherence", LessonKind.Video, Embed("2SI0twnNEN8"), 286),
                        Lesson(2, "Lesson 02 — Present Perfect review", LessonKind.Video, Embed("VY5nh_-1phQ"), 480),
                        Lesson(3, "Lesson 03 — Ever & never", LessonKind.Video, Embed("o-GWYDA4IQY"), 368)
                    )
                }
            };
        }

        private static Course BuildBusinessEnglish()
        {
            return new Course
            {
                Slug = "business-english",
                Title = "Business English",
                Type = CourseType.Standard,
                Level = "Intermediate",
                Duration = "8 weeks",
                Price = 59,
                Image = "images/course-business.jpg",
                Summary = "Sound clear and credible in meetings, presentations, and professional email.",
                Overview = "The course uses realistic workplace scenarios: project updates, negotiations, and client calls.",
                Video = Embed("m2UD0-IC7iY"),
                Outcomes = Outcomes(
                    "Lead a short meeting",
                    "Write concise professional email",
                    "Present results without reading slides",
                    "Handle disagreement calmly"
                ),
                Modules = new List<CourseModule>
                {
                    Module(1, "Module 01 — Meetings", "3 lessons · 30 min",
                        Lesson(1, "Lesson 01 — Speaking in meetings", LessonKind.Video, Embed("m2UD0-IC7iY"), 623),
                        Lesson(2, "Lesson 02 — Talking about meetings", LessonKind.Video, Embed("TL61VKkme14"), 360),
                        Lesson(3, "Lesson 03 — Professional presence", LessonKind.Video, Embed("eIho2S0ZahI"), 598)
                    )
                }
            };
        }

        private static Course BuildGerman()
        {
            return new Course
            {
                Slug = "german",
                Title = "German Language",
                Type = CourseType.Standard,
                Level = "Beginner",
                Duration = "10 weeks",
                Price = 59,
                Image = "images/course-german.jpg",
                Summary = "Start German with practical dialogues, clear grammar, and pronunciation you can trust.",
                Overview = "A1-focused classes cover sounds, cases in context, and the situations new arrivals need first.",
                Video = Embed("wpBPaDI5IgI"),
                Outcomes = Outcomes(
                    "Introduce yourself in German",
                    "Order, ask, and understand simple replies",
                    "Use articles and present tense with confidence",
                    "Read short everyday notices"
                ),
                Modules = new List<CourseModule>
                {
                    Module(1, "Module 01 — First contact", "2 lessons · 20 min",
                        Lesson(1, "Lesson 01 — Alphabet & phonetics", LessonKind.Video, Embed("wpBPaDI5IgI"), 800),
                        Lesson(2, "Lesson 02 — German alphabet A–Z", LessonKind.Video, Embed("xYuPIQMvEsg"), 155)
                    )
                }
            };
        }

        private static Course BuildSpanish()
        {
            return new Course
            {
                Slug = "spanish",
                Title = "Spanish Language",
                Type = CourseType.Standard,
                Level = "Beginner",
                Duration = "10 weeks",
                Price = 59,
                Image = "images/course-spanish.jpg",
                Summary = "Learn Spanish you can speak from the first class, with culture notes woven into every topic.",
                Overview = "Lessons balance conversation and grammar.",
                Video = Embed("J7frbFRIvoc"),
                Outcomes = Outcomes(
                    "Talk about yourself, family, and plans",
                    "Use present tense and gustar naturally",
                    "Navigate travel conversations",
                    "Understand slow, clear speech"
                ),
                Modules = new List<CourseModule>
                {
                    Module(1, "Module 01 — People and places", "3 lessons · 25 min",
                        Lesson(1, "Lesson 01 — Greetings & introductions", LessonKind.Video, Embed("J7frbFRIvoc"), 720),
                        Lesson(2, "Lesson 02 — Greetings vocabulary", LessonKind.Video, Embed("HHzWzVsKSQM"), 600),
                        Lesson(3, "Lesson 03 — Quick greetings", LessonKind.Video, Embed("CqN1ENPfaeQ"), 168)
                    )
                }
            };
        }

        private static Course BuildFrench()
        {
            return new Course
            {
                Slug = "french",
                Title = "French Beginner",
                Type = CourseType.Standard,
                Level = "Beginner",
                Duration = "10 weeks",
                Price = 59,
                Image = "images/course-french.jpg",
                Summary = "A friendly start in French, with pronunciation coaching and conversations for travel and study.",
                Overview = "You will get comfortable with sounds that feel new, then use them in café, travel, and classroom situations.",
                Video = Embed("vvidJedEQgY"),
                Outcomes = Outcomes(
                    "Pronounce French rhythms with more confidence",
                    "Introduce yourself and ask simple questions",
                    "Handle menus and tickets",
                    "Write a short personal message"
                ),
                Modules = new List<CourseModule>
                {
                    Module(1, "Module 01 — Sounds and greetings", "3 lessons · 35 min",
                        Lesson(1, "Lesson 01 — Pronunciation basics", LessonKind.Video, Embed("vvidJedEQgY"), 600),
                        Lesson(2, "Lesson 02 — Pronunciation rules", LessonKind.Video, Embed("YC23ulrY5Ms"), 549),
                        Lesson(3, "Lesson 03 — French vowel sounds", LessonKind.Video, Embed("hI2Pso1dDjM"), 883)
                    )
                }
            };
        }

        private static Course BuildConversation()
        {
            return new Course
            {
                Slug = "conversation",
                Title = "Conversation Workshop",
                Type = CourseType.Demo,
                Level = "All levels",
                Duration = "6 weeks",
                Price = 0,
                Image = "images/course-conversation.jpg",
                Summary = "A speaking-first workshop for learners who understand more than they say.",
                Overview = "Each session is built around a topic, useful chunks, and feedback on clarity.",
                Video = Embed("eIho2S0ZahI"),
                Outcomes = Outcomes(
                    "Speak for longer turns",
                    "Ask follow-up questions",
                    "Notice and reuse natural chunks",
                    "Track your own progress"
                ),
                Modules = new List<CourseModule>
                {
                    Module(1, "Module 01 — Confidence", "5 lessons · 50 min",
                        Lesson(1, "Lesson 01 — Speak so people listen", LessonKind.Video, Embed("eIho2S0ZahI"), 598),
                        Lesson(2, "Lesson 02 — Easy conversations", LessonKind.Video, Embed("I_tRSrPru94"), 360),
                        Lesson(3, "Lesson 03 — Small talk practice", LessonKind.Video, Embed("h9HvFZUgxiM"), 900),
                        Lesson(4, "Quiz - Speaking check", LessonKind.Quiz, null, 600),
                        Lesson(5, "Homework - Record a 1-minute talk", LessonKind.Homework, null, 0)
                    )
                }
            };
        }

        private static List<CourseOutcome> Outcomes(params string[] texts)
        {
            return texts.Select((text, index) => new CourseOutcome
            {
                Text = text,
                Order = index + 1
            }).ToList();
        }

        private static CourseModule Module(int order, string title, string meta, params Lesson[] lessons)
        {
            return new CourseModule
            {
                Title = title,
                Info = meta,
                Order = order,
                Lessons = lessons
            };
        }

        private static Lesson Lesson(int order, string title, LessonKind kind, string? videoUrl, int duration)
        {
            return new Lesson
            {
                Title = title,
                Kind = kind,
                Video = videoUrl,
                Seconds = duration,
                Order = order
            };
        }
    }
}
