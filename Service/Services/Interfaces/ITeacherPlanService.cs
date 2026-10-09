using Service.Helpers.DTOs.Plans;

namespace Service.Services.Interfaces
{
    public interface ITeacherPlanService
    {
        Task<PlanPageDto?> GetMineAsync(string userId);
        Task<PlanResultDto> ChooseAsync(string userId, PlanChooseDto dto);
    }
}
