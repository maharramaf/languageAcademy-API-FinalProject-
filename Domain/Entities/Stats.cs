using Domain.Common;


namespace Domain.Entities
{
    public class Stats : BaseEntity
    {
        public ICollection<StatItem> Items { get; set; } = new List<StatItem>();
    }
}
