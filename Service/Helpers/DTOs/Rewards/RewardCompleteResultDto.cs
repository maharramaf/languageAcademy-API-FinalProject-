namespace Service.Helpers.DTOs.Rewards
{
    public class RewardCompleteResultDto
    {
        public bool Succeeded { get; set; }
        public bool AlreadyCompleted { get; set; }
        public int XpGained { get; set; }
        public int PointsGained { get; set; }
        public string Kind { get; set; } = string.Empty;
        public bool CertificateReady { get; set; }
        public string? CourseSlug { get; set; }
        public IReadOnlyList<string> Errors { get; set; } = Array.Empty<string>();
    }
}
