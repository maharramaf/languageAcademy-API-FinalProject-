using Domain.Common;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Lesson : BaseEntity
    {
        public int CourseModuleId { get; set; }
        public CourseModule CourseModule { get; set; } = null!;
        public string Title { get; set; } = string.Empty;
        public LessonKind Kind { get; set; }
        public string? Video { get; set; }
        public int Seconds { get; set; }
        public int Order { get; set; }
    }
}
