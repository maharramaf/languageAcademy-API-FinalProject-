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
        public string Eyebrow { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string ParagraphOne { get; set; } = string.Empty;
        public string ParagraphTwo { get; set; } = string.Empty;
        public string FeatureOne { get; set; } = string.Empty;
        public string FeatureTwo { get; set; } = string.Empty;
        public string FeatureThree { get; set; } = string.Empty;
        public string FeatureFour { get; set; } = string.Empty;
        public string ButtonText { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public string ImageAlt { get; set; } = string.Empty;
        public string YearsStat { get; set; } = string.Empty;
        public string YearsLabelLineOne { get; set; } = string.Empty;
        public string YearsLabelLineTwo { get; set; } = string.Empty;
    }
}
