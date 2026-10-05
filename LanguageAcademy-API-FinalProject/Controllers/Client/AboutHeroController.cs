using Microsoft.AspNetCore.Mvc;
using Service.Services.Interfaces;

namespace LanguageAcademy_API_FinalProject.Controllers.Client
{
    [Route("api/[controller]")]
    [ApiController]
    public class AboutHeroController : ControllerBase
    {
        private readonly IAboutHeroService _aboutHeroService;

        public AboutHeroController(IAboutHeroService aboutHeroService)
        {
            _aboutHeroService = aboutHeroService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAboutHeroAsync()
        {
            var section = await _aboutHeroService.GetUIAsync();
            if (section is null) return NotFound();
            return Ok(section);
        }
    }
}
