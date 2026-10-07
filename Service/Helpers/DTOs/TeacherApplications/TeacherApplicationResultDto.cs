namespace Service.Helpers.DTOs.TeacherApplications
{
    public class TeacherApplicationResultDto
    {
        public bool Succeeded { get; set; }
        public IReadOnlyList<string> Errors { get; set; } = Array.Empty<string>();
        public string? TemporaryPassword { get; set; }
    }
}
