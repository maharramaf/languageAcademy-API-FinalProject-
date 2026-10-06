using Domain.Common;


namespace Domain.Entities
{
    public class About : BaseEntity
    {
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
        public string PageKey { get; set; } = "home";
    }
}
