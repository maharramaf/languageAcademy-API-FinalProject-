using Domain.Common;

namespace Domain.Entities
{
    public class Notification : BaseEntity
    {
        public string UserId { get; set; } = string.Empty;
        public AppUser User { get; set; } = null!;
        public string Type { get; set; } = "announcement";
        public string Title { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public string Href { get; set; } = string.Empty;
        public bool IsRead { get; set; }
    }
}
