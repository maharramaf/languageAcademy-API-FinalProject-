using Domain.Common;
using Domain.Enums;

namespace Domain.Entities
{
    public class TeacherApplication : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string Education { get; set; } = string.Empty;
        public string Institution { get; set; } = string.Empty;
        public string Experience { get; set; } = string.Empty;
        public int Years { get; set; }
        public string Languages { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Bio { get; set; } = string.Empty;
        public string Portfolio { get; set; } = string.Empty;
        public TeacherApplicationStatus Status { get; set; } = TeacherApplicationStatus.Pending;
    }
}
