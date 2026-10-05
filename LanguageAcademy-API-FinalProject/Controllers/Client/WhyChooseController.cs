using Microsoft.AspNetCore.Mvc;
using Service.Services.Interfaces;

namespace LanguageAcademy_API_FinalProject.Controllers.Client
{
    [Route("api/[controller]")]
    [ApiController]
    public class WhyChooseController : ControllerBase
    {
        private readonly IWhyChooseService _whyChooseService;

        public WhyChooseController(IWhyChooseService whyChooseService)
        {
            _whyChooseService = whyChooseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetWhyChooseAsync()
        {
            var section = await _whyChooseService.GetUIAsync();
            if (section is null) return NotFound();
            return Ok(section);
        }
    }
}
