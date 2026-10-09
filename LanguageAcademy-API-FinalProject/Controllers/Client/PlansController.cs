using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Helpers.DTOs.Plans;
using Service.Services.Interfaces;

namespace LanguageAcademy_API_FinalProject.Controllers.Client
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PlansController : ControllerBase
    {
        private readonly IPlanService _planService;

        public PlansController(IPlanService planService)
        {
            _planService = planService;
        }

        [HttpGet]
        public async Task<IActionResult> GetMineAsync()
        {
            var userId = UserId();
            if (userId is null) return Unauthorized();

            var page = await _planService.GetMineAsync(userId);
            if (page is null) return NotFound();
            return Ok(page);
        }

        [HttpPut]
        public async Task<IActionResult> ChooseAsync([FromBody] PlanChooseDto? dto)
        {
            if (dto is null) return BadRequest();

            var userId = UserId();
            if (userId is null) return Unauthorized();

            var result = await _planService.ChooseAsync(userId, dto);
            if (!result.Succeeded)
                return BadRequest(new { errors = result.Errors });

            return Ok(new { result.Current });
        }

        private string? UserId()
        {
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier)
                ?? User.FindFirstValue(JwtRegisteredClaimNames.Sub);
            return string.IsNullOrWhiteSpace(id) ? null : id;
        }
    }
}
