using Microsoft.AspNetCore.Mvc;
using Service.Services.Interfaces;

namespace LanguageAcademy_API_FinalProject.Controllers.Client
{
    [Route("api/[controller]")]
    [ApiController]
    public class HowWeWorkController : ControllerBase
    {
        private readonly IHowWeWorkService _howWeWorkService;

        public HowWeWorkController(IHowWeWorkService howWeWorkService)
        {
            _howWeWorkService = howWeWorkService;
        }

        [HttpGet]
        public async Task<IActionResult> GetHowWeWorkAsync()
        {
            var section = await _howWeWorkService.GetUIAsync();
            if (section is null) return NotFound();
            return Ok(section);
        }
    }
}
