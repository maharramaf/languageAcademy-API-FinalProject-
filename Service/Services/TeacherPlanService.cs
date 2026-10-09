using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Service.Helpers.DTOs.Plans;
using Service.Services.Interfaces;

namespace Service.Services
{
    public class TeacherPlanService : ITeacherPlanService
    {
        private readonly UserManager<AppUser> _userManager;

        public TeacherPlanService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<PlanPageDto?> GetMineAsync(string userId)
        {
            var user = await FindUserAsync(userId);
            if (user is null) return null;

            return new PlanPageDto
            {
                Current = user.TeacherPlan.ToString().ToLowerInvariant(),
                Items = Catalog()
            };
        }

        public async Task<PlanResultDto> ChooseAsync(string userId, PlanChooseDto dto)
        {
            if (!Enum.TryParse<CourseType>(dto.Type?.Trim(), true, out var type)
                || !Enum.IsDefined(type))
                return Fail("Teacher plan must be Demo, Standard, or Premium.");

            var user = await FindUserAsync(userId);
            if (user is null)
                return Fail("Account was not found.");

            user.TeacherPlan = type;
            var updated = await _userManager.UpdateAsync(user);
            if (!updated.Succeeded)
                return Fail(updated.Errors.Select(e => e.Description));

            return new PlanResultDto
            {
                Succeeded = true,
                Current = user.TeacherPlan.ToString().ToLowerInvariant()
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
                    Title = "Free Teacher",
                    Price = 0,
                    Info = "1 course. Demo billing only."
                },
                new()
                {
                    Type = "standard",
                    Title = "Standard Teacher",
                    Price = 19,
                    Info = "More courses. No card charge in this test."
                },
                new()
                {
                    Type = "premium",
                    Title = "Premium Teacher",
                    Price = 39,
                    Info = "Unlimited courses. No card charge in this test."
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
