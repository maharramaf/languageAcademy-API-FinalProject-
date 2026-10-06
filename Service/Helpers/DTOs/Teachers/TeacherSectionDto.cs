namespace Service.Helpers.DTOs.Teachers
{
    public class TeacherSectionDto
    {
        public int Id { get; set; }
        public string Eyebrow { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Lead { get; set; } = string.Empty;
        public List<TeacherDto> Teachers { get; set; } = new();
    }
}
