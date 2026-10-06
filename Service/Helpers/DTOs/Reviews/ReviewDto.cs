namespace Service.Helpers.DTOs.Reviews
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public int Rating { get; set; }
        public string Photo { get; set; } = string.Empty;
        public string PhotoAlt { get; set; } = string.Empty;
        public string Result { get; set; } = string.Empty;
        public string Course { get; set; } = string.Empty;
        public string CourseSlug { get; set; } = string.Empty;
    }
}
