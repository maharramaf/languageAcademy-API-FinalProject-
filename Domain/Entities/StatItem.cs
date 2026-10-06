using Domain.Common;


namespace Domain.Entities
{
    public class StatItem : BaseEntity
    {
        public int StatsId { get; set; }
        public Stats Stats { get; set; } = null!;
        public string SourceKey { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
        public string Label { get; set; } = string.Empty;
        public string Suffix { get; set; } = string.Empty;
        public int StoredValue { get; set; }
        public int SortOrder { get; set; }
    }
}
