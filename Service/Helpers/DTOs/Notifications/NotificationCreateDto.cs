namespace Service.Helpers.DTOs.Notifications
{
    public class NotificationCreateDto
    {
        public string UserId { get; set; } = string.Empty;
        public string Type { get; set; } = "announcement";
        public string Title { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public string Href { get; set; } = string.Empty;
    }
}
