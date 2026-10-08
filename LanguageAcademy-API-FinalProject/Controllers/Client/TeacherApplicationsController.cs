using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Helpers.DTOs.TeacherApplications;
using Service.Services.Interfaces;

namespace LanguageAcademy_API_FinalProject.Controllers.Client
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherApplicationsController : ControllerBase
    {
        private readonly ITeacherApplicationService _service;

        public TeacherApplicationsController(ITeacherApplicationService service)
        {
            _service = service;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ApplyAsync([FromBody] TeacherApplicationCreateDto? dto)
        {
            if (dto is null) return BadRequest();

            var result = await _service.ApplyAsync(dto);
            if (!result.Succeeded)
                return BadRequest(new { errors = result.Errors });

            return Ok();
        }
    }
}
