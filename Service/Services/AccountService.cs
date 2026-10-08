using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using Domain.Constants;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Service.Helpers.DTOs.Accounts;
using Service.Services.Interfaces;


namespace Service.Services
{
    public class AccountService : IAccountService
    {
        private const string LoginFailed = "Email or password is incorrect.";

        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;

        public AccountService(UserManager<AppUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<RegisterResultDto> RegisterStudentAsync(RegisterDto dto)
        {
            var errors = Validate(dto);
            if (errors.Count > 0)
                return Fail(errors);

            var email = dto.Email.Trim();
            if (await _userManager.FindByEmailAsync(email) is not null)
                return Fail("Email is already taken.");

            var user = new AppUser
            {
                Name = dto.Name.Trim(),
                Surname = dto.Surname.Trim(),
                Email = email,
                UserName = email,
                PhoneNumber = dto.Phone.Trim(),
                EmailConfirmed = true
            };

            var created = await _userManager.CreateAsync(user, dto.Password);
            if (!created.Succeeded)
                return Fail(created.Errors.Select(e => e.Description));

            var role = await _userManager.AddToRoleAsync(user, Roles.Student);
            if (!role.Succeeded)
            {
                await _userManager.DeleteAsync(user);
                return Fail(role.Errors.Select(e => e.Description));
            }

            return new RegisterResultDto
            {
                Succeeded = true,
                Email = user.Email ?? email,
                Name = user.Name,
                Surname = user.Surname,
                Role = Roles.Student
            };
        }

        public async Task<LoginResultDto> LoginAsync(LoginDto dto)
        {
            var email = (dto.Email ?? string.Empty).Trim();
            var password = dto.Password ?? string.Empty;

            if (email.Length is 0 or > 254 || !new EmailAddressAttribute().IsValid(email) || string.IsNullOrEmpty(password))
                return LoginFail(LoginFailed);

            var user = await _userManager.FindByEmailAsync(email);
            if (user is null)
                return LoginFail(LoginFailed);

            if (await _userManager.IsLockedOutAsync(user))
                return LoginFail(LoginFailed);

            if (!await _userManager.CheckPasswordAsync(user, password))
            {
                await _userManager.AccessFailedAsync(user);
                return LoginFail(LoginFailed);
            }

            await _userManager.ResetAccessFailedCountAsync(user);

            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault() ?? string.Empty;

            return new LoginResultDto
            {
                Succeeded = true,
                Token = CreateToken(user, roles),
                Email = user.Email ?? email,
                Name = user.Name,
                Surname = user.Surname,
                Role = role
            };
        }

        public async Task<List<StudentAccountDto>> GetStudentsAsync()
        {
            var users = await _userManager.GetUsersInRoleAsync(Roles.Student);
            var items = new List<StudentAccountDto>();

            foreach (var user in users.OrderByDescending(u => u.CreatedAt))
            {
                var roles = await _userManager.GetRolesAsync(user);
                if (roles.Contains(Roles.Teacher) || roles.Contains(Roles.Admin) || roles.Contains(Roles.SuperAdmin))
                    continue;

                items.Add(new StudentAccountDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    Surname = user.Surname,
                    Email = user.Email ?? string.Empty,
                    Phone = user.PhoneNumber ?? string.Empty,
                    CreatedAt = user.CreatedAt
                });
            }

            return items;
        }

        private string CreateToken(AppUser user, IList<string> roles)
        {
            var jwt = _configuration.GetSection("Jwt");
            var key = jwt["Key"] ?? throw new InvalidOperationException("Jwt:Key is missing.");
            var hours = int.TryParse(jwt["ExpireHours"], out var parsed) ? parsed : 8;

            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, user.Id),
                new(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
                new(ClaimTypes.NameIdentifier, user.Id),
                new(ClaimTypes.Email, user.Email ?? string.Empty),
                new(ClaimTypes.GivenName, user.Name),
                new(ClaimTypes.Surname, user.Surname),
                new(ClaimTypes.Name, $"{user.Name} {user.Surname}".Trim())
            };

            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role));

            var signing = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwt["Issuer"],
                audience: jwt["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(hours),
                signingCredentials: signing);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private static List<string> Validate(RegisterDto dto)
        {
            var errors = new List<string>();
            var name = (dto.Name ?? string.Empty).Trim();
            var surname = (dto.Surname ?? string.Empty).Trim();
            var email = (dto.Email ?? string.Empty).Trim();
            var phone = (dto.Phone ?? string.Empty).Trim();
            var password = dto.Password ?? string.Empty;
            var confirm = dto.ConfirmPassword ?? string.Empty;
            var digits = Regex.Replace(phone, @"\D", string.Empty);

            if (name.Length is 0 or > 80)
                errors.Add("Name is required.");
            if (surname.Length is 0 or > 80)
                errors.Add("Surname is required.");
            if (email.Length is 0 or > 254)
                errors.Add("Email is required.");
            else if (!new EmailAddressAttribute().IsValid(email))
                errors.Add("Enter a valid email address.");
            if (phone.Length is 0 or > 40 || digits.Length < 7)
                errors.Add("Enter a valid phone number.");
            if (string.IsNullOrEmpty(password))
                errors.Add("Password is required.");
            else if (password.Length < 8)
                errors.Add("Use at least 8 characters.");
            if (string.IsNullOrEmpty(confirm))
                errors.Add("Confirm your password.");
            else if (confirm != password)
                errors.Add("Passwords do not match.");

            return errors;
        }

        private static RegisterResultDto Fail(string error) => Fail(new[] { error });

        private static RegisterResultDto Fail(IEnumerable<string> errors)
        {
            return new RegisterResultDto
            {
                Succeeded = false,
                Errors = errors.ToList()
            };
        }

        private static LoginResultDto LoginFail(string error)
        {
            return new LoginResultDto
            {
                Succeeded = false,
                Errors = new[] { error }
            };
        }
    }
}
