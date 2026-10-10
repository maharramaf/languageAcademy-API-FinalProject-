namespace Service.Helpers.DTOs.Rewards
{
    public class RewardAchievementDto
    {
        public string Key { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public int Done { get; set; }
        public int Total { get; set; }
        public bool Unlocked { get; set; }
    }
}
