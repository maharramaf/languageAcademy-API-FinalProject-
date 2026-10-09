namespace Service.Helpers.DTOs.Plans
{
    public class PlanDto
    {
        public string Type { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Info { get; set; } = string.Empty;
    }
}
