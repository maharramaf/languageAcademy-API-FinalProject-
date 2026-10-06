using Microsoft.AspNetCore.Mvc;
using Service.Helpers.DTOs.Contacts;
using Service.Services.Interfaces;

namespace LanguageAcademy_API_FinalProject.Controllers.Client
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet]
        public async Task<IActionResult> GetContactAsync()
        {
            var section = await _contactService.GetUIAsync();
            if (section is null) return NotFound();
            return Ok(section);
        }

        [HttpPost]
        public async Task<IActionResult> SendAsync([FromBody] ContactSendDto? dto)
        {
            if (dto is null) return BadRequest();
            var ok = await _contactService.SendAsync(dto);
            if (!ok) return BadRequest();
            return Ok();
        }
    }
}
