using Domain.Common;


namespace Domain.Entities
{
    public class StudentSection : BaseEntity
    {
        public string Subtitle { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public string StoriesSubtitle { get; set; } = string.Empty;
        public string StoriesTitle { get; set; } = string.Empty;
        public string StoriesText { get; set; } = string.Empty;
    }
}
