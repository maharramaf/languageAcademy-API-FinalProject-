namespace Service.Helpers.DTOs.Stats
{
    public class StatsDto
    {
        public int Id { get; set; }
        public List<StatItemDto> Items { get; set; } = new();
    }
}
