namespace Service.Helpers.DTOs.Courses
{
    public class LessonCreateDto
    {
        public string Title { get; set; } = string.Empty;
        public string Kind { get; set; } = string.Empty;
        public int Seconds { get; set; }
    }
}
