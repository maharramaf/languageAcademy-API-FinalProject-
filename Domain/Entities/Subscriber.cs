using Domain.Common;


namespace Domain.Entities
{
    public class Subscriber : BaseEntity
    {
        public string Email { get; set; } = string.Empty;
        public bool Active { get; set; } = true;
    }
}
