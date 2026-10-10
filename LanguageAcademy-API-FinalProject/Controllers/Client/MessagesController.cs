using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using LanguageAcademy_API_FinalProject.Hubs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Service.Helpers.DTOs.Messages;
using Service.Services.Interfaces;

namespace LanguageAcademy_API_FinalProject.Controllers.Client
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MessagesController : ControllerBase
    {
        private readonly IMessageService _messageService;
        private readonly IHubContext<ChatHub> _hub;

        public MessagesController(IMessageService messageService, IHubContext<ChatHub> hub)
        {
            _messageService = messageService;
            _hub = hub;
        }

        [HttpGet]
        public async Task<IActionResult> GetInboxAsync()
        {
            var userId = UserId();
            if (userId is null) return Unauthorized();
            return Ok(await _messageService.GetInboxAsync(userId));
        }

        [HttpGet("contacts")]
        public async Task<IActionResult> GetContactsAsync()
        {
            var userId = UserId();
            if (userId is null) return Unauthorized();
            return Ok(await _messageService.GetContactsAsync(userId));
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetThreadAsync(string userId)
        {
            var me = UserId();
            if (me is null) return Unauthorized();

            var thread = await _messageService.GetThreadAsync(me, userId);
            if (thread is null) return NotFound();
            return Ok(thread);
        }

        [HttpPost]
        public async Task<IActionResult> SendAsync([FromBody] MessageCreateDto? dto)
        {
            if (dto is null) return BadRequest();

            var userId = UserId();
            if (userId is null) return Unauthorized();

            var result = await _messageService.SendAsync(userId, dto);
            if (!result.Succeeded || result.Message is null)
                return BadRequest(new { errors = result.Errors });

            await _hub.Clients.Users(result.Message.SenderId, result.Message.ReceiverId)
                .SendAsync("ReceiveMessage", result.Message);
            return Ok(result.Message);
        }

        private string? UserId()
        {
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier)
                ?? User.FindFirstValue(JwtRegisteredClaimNames.Sub);
            return string.IsNullOrWhiteSpace(id) ? null : id;
        }
    }
}
