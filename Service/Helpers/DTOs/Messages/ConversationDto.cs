namespace Service.Helpers.DTOs.Messages
{
    public class ConversationDto
    {
        public string UserId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string LastBody { get; set; } = string.Empty;
        public DateTime LastAt { get; set; }
        public int Unread { get; set; }
    }
}
