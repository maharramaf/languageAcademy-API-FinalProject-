using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Interfaces;

namespace LanguageAcademy_API_FinalProject.Controllers.Client
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationsController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetMineAsync()
        {
            var userId = UserId();
            if (userId is null) return Unauthorized();
            return Ok(await _notificationService.GetMineAsync(userId));
        }

        [HttpPut("{id:int}/read")]
        public async Task<IActionResult> MarkReadAsync(int id)
        {
            var userId = UserId();
            if (userId is null) return Unauthorized();
            if (!await _notificationService.MarkReadAsync(userId, id))
                return NotFound();
            return NoContent();
        }

        [HttpPut("read-all")]
        public async Task<IActionResult> MarkAllReadAsync()
        {
            var userId = UserId();
            if (userId is null) return Unauthorized();
            await _notificationService.MarkAllReadAsync(userId);
            return NoContent();
        }

        private string? UserId()
        {
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier)
                ?? User.FindFirstValue(JwtRegisteredClaimNames.Sub);
            return string.IsNullOrWhiteSpace(id) ? null : id;
        }
    }
}
