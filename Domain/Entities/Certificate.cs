using Domain.Common;

namespace Domain.Entities
{
    public class Certificate : BaseEntity
    {
        public string StudentId { get; set; } = string.Empty;
        public AppUser Student { get; set; } = null!;
        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;
        public string Number { get; set; } = string.Empty;
    }
}
