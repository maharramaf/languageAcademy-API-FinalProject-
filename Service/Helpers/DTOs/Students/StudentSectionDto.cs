using Service.Helpers.DTOs.Reviews;

namespace Service.Helpers.DTOs.Students
{
    public class StudentSectionDto
    {
        public int Id { get; set; }
        public string Subtitle { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public string StoriesSubtitle { get; set; } = string.Empty;
        public string StoriesTitle { get; set; } = string.Empty;
        public string StoriesText { get; set; } = string.Empty;
        public List<ReviewDto> Reviews { get; set; } = new();
    }
}
