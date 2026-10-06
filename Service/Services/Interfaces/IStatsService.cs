using Service.Helpers.DTOs.Stats;


namespace Service.Services.Interfaces
{
    public interface IStatsService
    {
        Task<StatsDto?> GetUIAsync();
    }
}
