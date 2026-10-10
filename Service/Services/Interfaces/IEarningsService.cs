using Service.Helpers.DTOs.Earnings;

namespace Service.Services.Interfaces
{
    public interface IEarningsService
    {
        Task<EarningsDto> GetMineAsync(string teacherId);
    }
}
