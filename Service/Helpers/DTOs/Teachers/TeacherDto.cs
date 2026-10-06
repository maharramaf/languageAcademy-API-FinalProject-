namespace Service.Helpers.DTOs.Teachers
{
    public class TeacherDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string Summary { get; set; } = string.Empty;
        public string Bio { get; set; } = string.Empty;
        public string Photo { get; set; } = string.Empty;
        public string PhotoAlt { get; set; } = string.Empty;
        public string LinkedIn { get; set; } = string.Empty;
        public string SocialIcon { get; set; } = string.Empty;
        public string Social { get; set; } = string.Empty;
        public int Order { get; set; }
    }
}
