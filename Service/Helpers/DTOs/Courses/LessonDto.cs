using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Helpers.DTOs.Courses
{
    public class LessonDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Kind { get; set; } = string.Empty;
        public string? VideoUrl { get; set; }
        public int DurationSeconds { get; set; }
        public int SortOrder { get; set; }
    }
}
