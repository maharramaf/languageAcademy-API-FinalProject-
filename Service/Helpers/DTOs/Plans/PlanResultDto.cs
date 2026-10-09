namespace Service.Helpers.DTOs.Plans
{
    public class PlanResultDto
    {
        public bool Succeeded { get; set; }
        public IReadOnlyList<string> Errors { get; set; } = Array.Empty<string>();
        public string Current { get; set; } = string.Empty;
    }
}
