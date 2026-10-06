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
        public string Info { get; set; } = string.Empty;
        public int Order { get; set; }
        public List<LessonDto> Lessons { get; set; } = new();
    }
}
