using Domain.Common;


namespace Domain.Entities
{
    public class TeacherSection : BaseEntity
    {
        public string Eyebrow { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Lead { get; set; } = string.Empty;
        public ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();
    }
}
