using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Hero : BaseEntity
    {
        public string Subtitle { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public string Button1 { get; set; } = string.Empty;
        public string Button2 { get; set; } = string.Empty;
        public string Point1 { get; set; } = string.Empty;
        public string Point2 { get; set; } = string.Empty;
        public string Point3 { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public string ImageAlt { get; set; } = string.Empty;
        public string Students { get; set; } = string.Empty;
        public string StudentsText { get; set; } = string.Empty;
        public string Rating { get; set; } = string.Empty;
        public string RatingText { get; set; } = string.Empty;
    }
}
