using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Helpers.DTOs.Accounts;
using Service.Services.Interfaces;

namespace LanguageAcademy_API_FinalProject.Controllers.Client
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterDto? dto)
        {
            if (dto is null) return BadRequest();

            var result = await _accountService.RegisterStudentAsync(dto);
            if (!result.Succeeded)
                return BadRequest(new { errors = result.Errors });

            return Ok(new
            {
                result.Email,
                result.Name,
                result.Surname,
                result.Role
            });
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync([FromBody] LoginDto? dto)
        {
            if (dto is null) return BadRequest();

            var result = await _accountService.LoginAsync(dto);
            if (!result.Succeeded)
                return Unauthorized(new { errors = result.Errors });

            return Ok(new
            {
                result.Token,
                result.Email,
                result.Name,
                result.Surname,
                result.Role
            });
        }

        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> GetMeAsync()
        {
            var userId = UserId();
            if (userId is null) return Unauthorized();

            var profile = await _accountService.GetProfileAsync(userId);
            if (profile is null) return NotFound();
            return Ok(profile);
        }

        [HttpPut("profile")]
        [Authorize]
        public async Task<IActionResult> UpdateProfileAsync([FromBody] ProfileUpdateDto? dto)
        {
            if (dto is null) return BadRequest();

            var userId = UserId();
            if (userId is null) return Unauthorized();

            var result = await _accountService.UpdateProfileAsync(userId, dto);
            if (!result.Succeeded)
                return BadRequest(new { errors = result.Errors });

            return Ok(new { result.Token });
        }

        [HttpPut("password")]
        [Authorize]
        public async Task<IActionResult> ChangePasswordAsync([FromBody] ChangePasswordDto? dto)
        {
            if (dto is null) return BadRequest();

            var userId = UserId();
            if (userId is null) return Unauthorized();

            var result = await _accountService.ChangePasswordAsync(userId, dto);
            if (!result.Succeeded)
                return BadRequest(new { errors = result.Errors });

            return Ok();
        }

        private string? UserId()
        {
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier)
                ?? User.FindFirstValue(JwtRegisteredClaimNames.Sub);
            return string.IsNullOrWhiteSpace(id) ? null : id;
        }
    }
}
