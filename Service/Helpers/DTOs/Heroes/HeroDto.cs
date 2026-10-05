using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Helpers.DTOs.Heroes
{
    public class HeroDto
    {
        public int Id { get; set; }
        public string Eyebrow { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Lead { get; set; } = string.Empty;
        public string PrimaryButtonText { get; set; } = string.Empty;
        public string SecondaryButtonText { get; set; } = string.Empty;
        public string PointOne { get; set; } = string.Empty;
        public string PointTwo { get; set; } = string.Empty;
        public string PointThree { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public string ImageAlt { get; set; } = string.Empty;
        public string StudentsStat { get; set; } = string.Empty;
        public string StudentsLabel { get; set; } = string.Empty;
        public string RatingStat { get; set; } = string.Empty;
        public string RatingLabel { get; set; } = string.Empty;
    }
}
