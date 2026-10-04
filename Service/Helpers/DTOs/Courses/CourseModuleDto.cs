using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Helpers.DTOs.Courses
{
    public class CourseModuleDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Meta { get; set; } = string.Empty;
        public int SortOrder { get; set; }
        public List<LessonDto> Lessons { get; set; } = new();
    }
}
