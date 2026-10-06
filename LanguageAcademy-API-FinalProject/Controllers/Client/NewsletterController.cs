using Microsoft.AspNetCore.Mvc;
using Service.Helpers.DTOs.Newsletters;
using Service.Services.Interfaces;

namespace LanguageAcademy_API_FinalProject.Controllers.Client
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsletterController : ControllerBase
    {
        private readonly INewsletterService _newsletterService;

        public NewsletterController(INewsletterService newsletterService)
        {
            _newsletterService = newsletterService;
        }

        [HttpGet]
        public async Task<IActionResult> GetNewsletterAsync()
        {
            var section = await _newsletterService.GetUIAsync();
            if (section is null) return NotFound();
            return Ok(section);
        }

        [HttpPost]
        public async Task<IActionResult> SubscribeAsync([FromBody] SubscribeDto? dto)
        {
            var ok = await _newsletterService.SubscribeAsync(dto?.Email ?? string.Empty);
            if (!ok) return BadRequest();
            return Ok();
        }
    }
}
