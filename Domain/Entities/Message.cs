using Domain.Common;

namespace Domain.Entities
{
    public class Message : BaseEntity
    {
        public string SenderId { get; set; } = string.Empty;
        public AppUser Sender { get; set; } = null!;
        public string ReceiverId { get; set; } = string.Empty;
        public AppUser Receiver { get; set; } = null!;
        public string Body { get; set; } = string.Empty;
        public bool IsRead { get; set; }
    }
}
