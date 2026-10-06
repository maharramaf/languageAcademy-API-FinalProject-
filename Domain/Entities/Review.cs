using Domain.Common;


namespace Domain.Entities
{
    public class Review : BaseEntity
    {
        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;
        public string Name { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public int Rating { get; set; }
        public string Photo { get; set; } = string.Empty;
        public string PhotoAlt { get; set; } = string.Empty;
        public bool Home { get; set; }
        public bool Approved { get; set; } = true;
        public int Order { get; set; }
    }
}
