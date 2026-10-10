using Service.Helpers.DTOs.Rewards;

namespace Service.Services.Interfaces
{
    public interface IRewardService
    {
        Task<RewardsDto?> GetMineAsync(string userId);
    }
}
