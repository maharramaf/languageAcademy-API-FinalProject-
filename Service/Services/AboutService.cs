
using Repository.Repositories.Interfaces;
using Service.Helpers.DTOs.Abouts;
using Service.Services.Interfaces;


namespace Service.Services
{
    public class AboutService : IAboutService
    {
        private readonly IAboutRepository _aboutRepo;
        public AboutService(IAboutRepository aboutRepo)
        {
            _aboutRepo = aboutRepo;
        }

        public async Task<AboutDto?> GetUIAsync(string page = "home")
        {
            var pageKey = string.Equals(page, "about", StringComparison.OrdinalIgnoreCase) ? "about" : "home";
            var about = await _aboutRepo.GetByPageKeyAsync(pageKey);
            if (about is null) return null;

            return new AboutDto
            {
                Id = about.Id,
                Eyebrow = about.Eyebrow,
                Title = about.Title,
                ParagraphOne = about.ParagraphOne,
                ParagraphTwo = about.ParagraphTwo,
                FeatureOne = about.FeatureOne,
                FeatureTwo = about.FeatureTwo,
                FeatureThree = about.FeatureThree,
                FeatureFour = about.FeatureFour,
                ButtonText = about.ButtonText,
                Image = about.Image,
                ImageAlt = about.ImageAlt,
                YearsStat = about.YearsStat,
                YearsLabelLineOne = about.YearsLabelLineOne,
                YearsLabelLineTwo = about.YearsLabelLineTwo
            };
        }
    }
}
