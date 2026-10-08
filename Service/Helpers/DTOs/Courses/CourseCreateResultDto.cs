namespace Service.Helpers.DTOs.Courses
{
    public class CourseCreateResultDto
    {
        public bool Succeeded { get; set; }
        public IReadOnlyList<string> Errors { get; set; } = Array.Empty<string>();
        public string Slug { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
    }
}
