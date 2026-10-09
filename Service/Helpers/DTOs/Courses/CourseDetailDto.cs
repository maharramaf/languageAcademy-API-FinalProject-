using Service.Helpers.DTOs.Reviews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Helpers.DTOs.Courses
{
    public class CourseDetailDto
    {
        public int Id { get; set; }
        public string Slug { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Level { get; set; } = string.Empty;
        public string Duration { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Image { get; set; } = string.Empty;
        public string Summary { get; set; } = string.Empty;
        public string Overview { get; set; } = string.Empty;
        public string? Video { get; set; }
        public string? TeacherEmail { get; set; }
        public List<string> Outcomes { get; set; } = new();
        public List<ReviewDto> Reviews { get; set; } = new();
        public List<CourseModuleDto> Modules { get; set; } = new();
    }
}
