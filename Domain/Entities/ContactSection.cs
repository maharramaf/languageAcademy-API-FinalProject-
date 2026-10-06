using Domain.Common;


namespace Domain.Entities
{
    public class ContactSection : BaseEntity
    {
        public string Subtitle { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Hours { get; set; } = string.Empty;
        public string Saturday { get; set; } = string.Empty;
    }
}
