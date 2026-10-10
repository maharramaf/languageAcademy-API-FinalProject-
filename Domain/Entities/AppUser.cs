using Domain.Enums;
using Microsoft.AspNetCore.Identity;


namespace Domain.Entities
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public CourseType Plan { get; set; } = CourseType.Demo;
        public CourseType TeacherPlan { get; set; } = CourseType.Demo;
        public int RewardXp { get; set; }
        public int RewardPoints { get; set; }
        public int RewardLessons { get; set; }
        public int RewardHomework { get; set; }
        public int RewardQuizzes { get; set; }
        public int RewardStreakDays { get; set; }
        public DateTime? RewardLastActive { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
