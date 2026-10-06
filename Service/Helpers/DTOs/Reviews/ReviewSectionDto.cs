namespace Service.Helpers.DTOs.Reviews
{
    public class ReviewSectionDto
    {
        public int Id { get; set; }
        public string Subtitle { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public List<ReviewDto> Reviews { get; set; } = new();
    }
}
