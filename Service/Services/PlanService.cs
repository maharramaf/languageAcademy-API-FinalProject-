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
        private readonly INotificationService _notificationService;

        public PlanService(UserManager<AppUser> userManager, INotificationService notificationService)
        {
            _userManager = userManager;
            _notificationService = notificationService;
        }

        public async Task<PlanPageDto?> GetMineAsync(string userId)
        {
            var user = await FindUserAsync(userId);
            if (user is null) return null;

            var items = Catalog();
            var current = user.Plan.ToString().ToLowerInvariant();
            return new PlanPageDto
            {
                Current = current,
                CurrentTitle = items.FirstOrDefault(item => item.Type == current)?.Title ?? user.Plan.ToString(),
                Items = items
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

            var current = user.Plan.ToString().ToLowerInvariant();
            await _notificationService.AddAsync(new Service.Helpers.DTOs.Notifications.NotificationCreateDto
            {
                UserId = userId,
                Type = "announcement",
                Title = "Plan updated",
                Body = $"Your membership is now {user.Plan}.",
                Href = "/Dashboard/Plans"
            });

            return new PlanResultDto
            {
                Succeeded = true,
                Current = current
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
                    Info = "Start with demo courses. No card is charged.",
                    Features = new List<string>
                    {
                        "Demo courses only",
                        "Certificates when a demo course is complete",
                        "Rewards XP and MF Points",
                        "Test membership, no card"
                    }
                },
                new()
                {
                    Type = "standard",
                    Title = "Standard",
                    Price = 29,
                    Info = "Unlock Standard courses. No card charge in this test.",
                    Features = new List<string>
                    {
                        "Demo and Standard courses",
                        "Certificates when a course is complete",
                        "Messages with your teacher",
                        "Rewards XP and MF Points",
                        "Test membership, no card"
                    }
                },
                new()
                {
                    Type = "premium",
                    Title = "Premium",
                    Price = 49,
                    Info = "All course types. No card charge in this test.",
                    Features = new List<string>
                    {
                        "All course types, including Premium",
                        "Certificates when a course is complete",
                        "Messages with your teacher",
                        "Priority classroom access",
                        "Rewards XP and MF Points",
                        "Test membership, no card"
                    }
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
