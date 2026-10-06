namespace Service.Helpers.DTOs.Newsletters
{
    public class NewsletterSectionDto
    {
        public int Id { get; set; }
        public string Subtitle { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
    }
}
