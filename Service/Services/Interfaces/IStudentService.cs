using Service.Helpers.DTOs.Students;


namespace Service.Services.Interfaces
{
    public interface IStudentService
    {
        Task<StudentSectionDto?> GetUIAsync();
    }
}
