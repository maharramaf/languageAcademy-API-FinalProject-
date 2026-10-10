namespace Service.Helpers.DTOs.Messages
{
    public class MessageDto
    {
        public int Id { get; set; }
        public string SenderId { get; set; } = string.Empty;
        public string ReceiverId { get; set; } = string.Empty;
        public string SenderName { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public bool Mine { get; set; }
    }
}
