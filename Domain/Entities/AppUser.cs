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
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
