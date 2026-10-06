using Domain.Common;


namespace Domain.Entities
{
    public class About : BaseEntity
    {
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
        public string PageKey { get; set; } = "home";
    }
}
