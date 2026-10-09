using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Helpers.DTOs.Enrollments;
using Service.Services.Interfaces;

namespace LanguageAcademy_API_FinalProject.Controllers.Client
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = Roles.Student)]
    public class EnrollmentsController : ControllerBase
    {
        private readonly IEnrollmentService _enrollmentService;

        public EnrollmentsController(IEnrollmentService enrollmentService)
        {
            _enrollmentService = enrollmentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetMineAsync()
        {
            var studentId = StudentId();
            if (studentId is null) return Unauthorized();

            return Ok(await _enrollmentService.GetMineAsync(studentId));
        }

        [HttpGet("{courseId:int}")]
        public async Task<IActionResult> GetStatusAsync(int courseId)
        {
            var studentId = StudentId();
            if (studentId is null) return Unauthorized();

            return Ok(new { enrolled = await _enrollmentService.IsEnrolledAsync(studentId, courseId) });
        }

        [HttpPost]
        public async Task<IActionResult> EnrollAsync([FromBody] EnrollmentCreateDto? dto)
        {
            if (dto is null) return BadRequest();

            var studentId = StudentId();
            if (studentId is null) return Unauthorized();

            var result = await _enrollmentService.EnrollAsync(studentId, dto);
            if (!result.Succeeded)
            {
                if (result.Errors.Any(e => e.EndsWith("was not found.")))
                    return NotFound();
                return BadRequest(new { errors = result.Errors });
            }

            return Ok(new { result.Slug, result.Title });
        }

        private string? StudentId()
        {
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier)
                ?? User.FindFirstValue(JwtRegisteredClaimNames.Sub);
            return string.IsNullOrWhiteSpace(id) ? null : id;
        }
    }
}
