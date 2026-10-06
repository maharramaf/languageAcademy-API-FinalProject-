
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
                Subtitle = about.Subtitle,
                Title = about.Title,
                Text1 = about.Text1,
                Text2 = about.Text2,
                Feature1 = about.Feature1,
                Feature2 = about.Feature2,
                Feature3 = about.Feature3,
                Feature4 = about.Feature4,
                Button = about.Button,
                Image = about.Image,
                ImageAlt = about.ImageAlt,
                Years = about.Years,
                YearsText1 = about.YearsText1,
                YearsText2 = about.YearsText2
            };
        }
    }
}
