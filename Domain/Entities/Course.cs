using Domain.Common;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Course : BaseEntity
    {
        public string Slug { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public CourseType Type { get; set; }
        public string Level { get; set; } = string.Empty;
        public string Duration { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Image { get; set; } = string.Empty;
        public string Summary { get; set; } = string.Empty;
        public string Overview { get; set; } = string.Empty;
        public string? Video { get; set; }
        public ICollection<CourseModule> Modules { get; set; } = new List<CourseModule>();
        public ICollection<CourseOutcome> Outcomes { get; set; } = new List<CourseOutcome>();
    }
}
