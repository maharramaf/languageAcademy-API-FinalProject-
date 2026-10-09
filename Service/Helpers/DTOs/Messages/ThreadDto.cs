namespace Service.Helpers.DTOs.Messages
{
    public class ThreadDto
    {
        public string UserId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public List<MessageDto> Messages { get; set; } = new();
    }
}
