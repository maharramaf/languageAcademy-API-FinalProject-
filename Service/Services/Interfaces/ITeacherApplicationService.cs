using Service.Helpers.DTOs.TeacherApplications;

namespace Service.Services.Interfaces
{
    public interface ITeacherApplicationService
    {
        Task<TeacherApplicationResultDto> ApplyAsync(TeacherApplicationCreateDto dto);
        Task<IReadOnlyList<TeacherApplicationDto>> GetAllAsync();
    }
}
