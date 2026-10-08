using Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Interfaces;

namespace LanguageAcademy_API_FinalProject.Controllers.Admin
{
    [Route("api/admin/[controller]")]
    [ApiController]
    [Authorize(Roles = Roles.Admin + "," + Roles.SuperAdmin)]
    public class TeacherApplicationsController : ControllerBase
    {
        private readonly ITeacherApplicationService _service;

        public TeacherApplicationsController(ITeacherApplicationService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpPost("{id:int}/accept")]
        public async Task<IActionResult> AcceptAsync(int id)
        {
            var result = await _service.AcceptAsync(id);
            if (!result.Succeeded)
                return BadRequest(new { errors = result.Errors });

            return Ok(new { temporaryPassword = result.TemporaryPassword });
        }

        [HttpPost("{id:int}/reject")]
        public async Task<IActionResult> RejectAsync(int id)
        {
            var result = await _service.RejectAsync(id);
            if (!result.Succeeded)
                return BadRequest(new { errors = result.Errors });

            return Ok();
        }
    }
}
