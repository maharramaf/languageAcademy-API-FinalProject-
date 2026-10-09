namespace Service.Helpers.DTOs.Courses
{
    public class CourseUpdateDto
    {
        public string Title { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Level { get; set; } = string.Empty;
        public string Duration { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string? Image { get; set; }
        public string Summary { get; set; } = string.Empty;
        public string Overview { get; set; } = string.Empty;
        public string? TeacherEmail { get; set; }
    }
}
