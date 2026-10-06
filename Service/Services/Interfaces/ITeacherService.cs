using Service.Helpers.DTOs.Teachers;


namespace Service.Services.Interfaces
{
    public interface ITeacherService
    {
        Task<TeacherSectionDto?> GetUIAsync();
    }
}
