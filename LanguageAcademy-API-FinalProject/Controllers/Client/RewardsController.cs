using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Interfaces;

namespace LanguageAcademy_API_FinalProject.Controllers.Client
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = Roles.Student + "," + Roles.Admin + "," + Roles.SuperAdmin)]
    public class RewardsController : ControllerBase
    {
        private readonly IRewardService _rewardService;

        public RewardsController(IRewardService rewardService)
        {
            _rewardService = rewardService;
        }

        [HttpGet]
        public async Task<IActionResult> GetMineAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)
                ?? User.FindFirstValue(JwtRegisteredClaimNames.Sub);
            if (string.IsNullOrWhiteSpace(userId))
                return Unauthorized();

            var page = await _rewardService.GetMineAsync(userId);
            if (page is null) return NotFound();
            return Ok(page);
        }

        [HttpPost("lessons/{lessonId:int}")]
        public async Task<IActionResult> CompleteLessonAsync(int lessonId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)
                ?? User.FindFirstValue(JwtRegisteredClaimNames.Sub);
            if (string.IsNullOrWhiteSpace(userId))
                return Unauthorized();

            var result = await _rewardService.CompleteLessonAsync(userId, lessonId);
            if (!result.Succeeded)
                return BadRequest(new { errors = result.Errors });

            return Ok(new { result.AlreadyCompleted, result.XpGained, result.PointsGained, result.Kind });
        }
    }
}
