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
        public string LinkedInUrl { get; set; } = string.Empty;
        public string LinkedInAriaLabel { get; set; } = string.Empty;
        public string SocialIcon { get; set; } = string.Empty;
        public string SocialUrl { get; set; } = string.Empty;
        public string SocialAriaLabel { get; set; } = string.Empty;
        public string ButtonText { get; set; } = string.Empty;
        public int SortOrder { get; set; }
    }
}
