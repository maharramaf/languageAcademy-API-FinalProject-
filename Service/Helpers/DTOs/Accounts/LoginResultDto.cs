namespace Service.Helpers.DTOs.Accounts
{
    public class LoginResultDto
    {
        public bool Succeeded { get; set; }
        public IReadOnlyList<string> Errors { get; set; } = Array.Empty<string>();
        public string Token { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
}
