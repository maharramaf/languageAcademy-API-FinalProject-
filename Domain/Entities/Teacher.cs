using Domain.Common;


namespace Domain.Entities
{
    public class Teacher : BaseEntity
    {
        public int TeacherSectionId { get; set; }
        public TeacherSection TeacherSection { get; set; } = null!;
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
