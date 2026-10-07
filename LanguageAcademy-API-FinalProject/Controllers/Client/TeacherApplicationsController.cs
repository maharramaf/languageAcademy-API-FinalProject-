using Domain.Constants;
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

        [HttpGet]
        [Authorize(Roles = Roles.Admin + "," + Roles.SuperAdmin)]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _service.GetAllAsync());
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

        [HttpPost("{id:int}/accept")]
        [Authorize(Roles = Roles.Admin + "," + Roles.SuperAdmin)]
        public async Task<IActionResult> AcceptAsync(int id)
        {
            var result = await _service.AcceptAsync(id);
            if (!result.Succeeded)
                return BadRequest(new { errors = result.Errors });

            return Ok(new { temporaryPassword = result.TemporaryPassword });
        }

        [HttpPost("{id:int}/reject")]
        [Authorize(Roles = Roles.Admin + "," + Roles.SuperAdmin)]
        public async Task<IActionResult> RejectAsync(int id)
        {
            var result = await _service.RejectAsync(id);
            if (!result.Succeeded)
                return BadRequest(new { errors = result.Errors });

            return Ok();
        }
    }
}
