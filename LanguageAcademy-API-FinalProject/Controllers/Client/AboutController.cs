using Microsoft.AspNetCore.Mvc;
using Service.Services.Interfaces;

namespace LanguageAcademy_API_FinalProject.Controllers.Client
{
    [Route("api/[controller]")]
    [ApiController]
    public class AboutController : ControllerBase
    {
        private readonly IAboutService _aboutService;

        public AboutController(IAboutService aboutService)
        {
            _aboutService = aboutService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAboutAsync([FromQuery] string page = "home")
        {
            var about = await _aboutService.GetUIAsync(page);
            if (about is null) return NotFound();
            return Ok(about);
        }
    }
}
