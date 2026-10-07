using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Domain.Constants;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Service.Helpers.DTOs.Accounts;
using Service.Services.Interfaces;


namespace Service.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;

        public AccountService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
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
    }
}
