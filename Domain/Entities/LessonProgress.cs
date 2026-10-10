using Domain.Common;

namespace Domain.Entities
{
    public class LessonProgress : BaseEntity
    {
        public string StudentId { get; set; } = string.Empty;
        public AppUser Student { get; set; } = null!;
        public int LessonId { get; set; }
        public Lesson Lesson { get; set; } = null!;
    }
}
