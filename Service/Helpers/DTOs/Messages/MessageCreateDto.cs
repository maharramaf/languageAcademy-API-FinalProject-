namespace Service.Helpers.DTOs.Messages
{
    public class MessageCreateDto
    {
        public string ReceiverId { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
    }
}
