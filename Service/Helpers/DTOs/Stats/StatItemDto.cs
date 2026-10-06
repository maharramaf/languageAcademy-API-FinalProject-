namespace Service.Helpers.DTOs.Stats
{
    public class StatItemDto
    {
        public int Id { get; set; }
        public string Icon { get; set; } = string.Empty;
        public int Value { get; set; }
        public string Suffix { get; set; } = string.Empty;
        public string Label { get; set; } = string.Empty;
        public int SortOrder { get; set; }
    }
}
