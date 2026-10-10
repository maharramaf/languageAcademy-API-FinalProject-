namespace Service.Helpers.DTOs.Plans
{
    public class PlanPageDto
    {
        public string Current { get; set; } = "demo";
        public string CurrentTitle { get; set; } = "Demo";
        public int AssignedCount { get; set; }
        public int? CourseLimit { get; set; }
        public bool ShowUsage { get; set; }
        public List<PlanDto> Items { get; set; } = new();
    }
}
