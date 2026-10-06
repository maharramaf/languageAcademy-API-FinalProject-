using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Helpers.DTOs.Abouts
{
    public class AboutDto
    {
        public int Id { get; set; }
        public string Subtitle { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Text1 { get; set; } = string.Empty;
        public string Text2 { get; set; } = string.Empty;
        public string Feature1 { get; set; } = string.Empty;
        public string Feature2 { get; set; } = string.Empty;
        public string Feature3 { get; set; } = string.Empty;
        public string Feature4 { get; set; } = string.Empty;
        public string Button { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public string ImageAlt { get; set; } = string.Empty;
        public string Years { get; set; } = string.Empty;
        public string YearsText1 { get; set; } = string.Empty;
        public string YearsText2 { get; set; } = string.Empty;
    }
}
