namespace Service.Helpers.DTOs.Plans
{
    public class PlanPageDto
    {
        public string Current { get; set; } = "demo";
        public List<PlanDto> Items { get; set; } = new();
    }
}
