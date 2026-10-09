using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Service.Helpers.DTOs.Plans;
using Service.Services.Interfaces;

namespace Service.Services
{
    public class PlanService : IPlanService
    {
        private readonly UserManager<AppUser> _userManager;

        public PlanService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<PlanPageDto?> GetMineAsync(string userId)
        {
            var user = await FindUserAsync(userId);
            if (user is null) return null;

            return new PlanPageDto
            {
                Current = user.Plan.ToString().ToLowerInvariant(),
                Items = Catalog()
            };
        }

        public async Task<PlanResultDto> ChooseAsync(string userId, PlanChooseDto dto)
        {
            if (!Enum.TryParse<CourseType>(dto.Type?.Trim(), true, out var type)
                || !Enum.IsDefined(type))
                return Fail("Plan must be Demo, Standard, or Premium.");

            var user = await FindUserAsync(userId);
            if (user is null)
                return Fail("Account was not found.");

            user.Plan = type;
            var updated = await _userManager.UpdateAsync(user);
            if (!updated.Succeeded)
                return Fail(updated.Errors.Select(e => e.Description));

            return new PlanResultDto
            {
                Succeeded = true,
                Current = user.Plan.ToString().ToLowerInvariant()
            };
        }

        private async Task<AppUser?> FindUserAsync(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                return null;

            return await _userManager.FindByIdAsync(userId);
        }

        private static List<PlanDto> Catalog()
        {
            return new List<PlanDto>
            {
                new()
                {
                    Type = "demo",
                    Title = "Demo",
                    Price = 0,
                    Info = "Free access to demo courses."
                },
                new()
                {
                    Type = "standard",
                    Title = "Standard",
                    Price = 29,
                    Info = "Standard courses. No card charge in this test."
                },
                new()
                {
                    Type = "premium",
                    Title = "Premium",
                    Price = 49,
                    Info = "All courses. No card charge in this test."
                }
            };
        }

        private static PlanResultDto Fail(string error) => Fail(new[] { error });

        private static PlanResultDto Fail(IEnumerable<string> errors)
        {
            return new PlanResultDto
            {
                Succeeded = false,
                Errors = errors.ToList()
            };
        }
    }
}
