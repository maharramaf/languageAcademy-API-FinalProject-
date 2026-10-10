namespace Service.Helpers.DTOs.Notifications
{
    public class NotificationDto
    {
        public int Id { get; set; }
        public string Type { get; set; } = "announcement";
        public string Title { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public string Href { get; set; } = string.Empty;
        public bool Read { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
