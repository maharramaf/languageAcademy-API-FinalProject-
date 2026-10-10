namespace Service.Helpers.DTOs.Rewards
{
    public class RewardsDto
    {
        public int Xp { get; set; }
        public int Points { get; set; }
        public int Level { get; set; }
        public string LevelName { get; set; } = string.Empty;
        public int ProgressPercent { get; set; }
        public string NextLevelText { get; set; } = string.Empty;
        public int StreakDays { get; set; }
        public int LessonsDone { get; set; }
        public int HomeworkDone { get; set; }
        public int QuizzesDone { get; set; }
        public int CoursesDone { get; set; }
        public List<RewardAchievementDto> Achievements { get; set; } = new();
    }
}
