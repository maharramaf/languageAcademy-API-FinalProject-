namespace Service.Helpers.DTOs.Messages
{
    public class MessageResultDto
    {
        public bool Succeeded { get; set; }
        public MessageDto? Message { get; set; }
        public IReadOnlyList<string> Errors { get; set; } = Array.Empty<string>();
    }
}
