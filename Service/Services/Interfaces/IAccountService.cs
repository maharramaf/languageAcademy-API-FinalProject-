using Service.Helpers.DTOs.Accounts;


namespace Service.Services.Interfaces
{
    public interface IAccountService
    {
        Task<RegisterResultDto> RegisterStudentAsync(RegisterDto dto);
        Task<LoginResultDto> LoginAsync(LoginDto dto);
        Task<List<StudentAccountDto>> GetStudentsAsync();
        Task<ProfileDto?> GetProfileAsync(string userId);
        Task<ProfileResultDto> UpdateProfileAsync(string userId, ProfileUpdateDto dto);
        Task<ProfileResultDto> ChangePasswordAsync(string userId, ChangePasswordDto dto);
    }
}
