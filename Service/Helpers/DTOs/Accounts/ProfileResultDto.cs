namespace Service.Helpers.DTOs.Accounts
{
    public class ProfileResultDto
    {
        public bool Succeeded { get; set; }
        public IReadOnlyList<string> Errors { get; set; } = Array.Empty<string>();
        public string? Token { get; set; }
    }
}
