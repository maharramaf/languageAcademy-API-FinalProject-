using Microsoft.AspNetCore.Mvc;
using Service.Services.Interfaces;

namespace LanguageAcademy_API_FinalProject.Controllers.Client
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeroController : ControllerBase
    {
        private readonly IHeroService _heroService;

        public HeroController(IHeroService heroService)
        {
            _heroService = heroService;
        }

        [HttpGet]
        public async Task<IActionResult> GetHeroAsync()
        {
            var hero = await _heroService.GetUIAsync();
            if (hero is null) return NotFound();
            return Ok(hero);
        }
    }
}
