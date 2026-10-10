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
        public string? Video { get; set; }
        public int Seconds { get; set; }
        public int Order { get; set; }
        public bool Completed { get; set; }
    }
}
