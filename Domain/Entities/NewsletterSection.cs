using Domain.Common;


namespace Domain.Entities
{
    public class NewsletterSection : BaseEntity
    {
        public string Subtitle { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
    }
}
