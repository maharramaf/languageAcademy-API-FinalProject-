using Service.Helpers.DTOs.Accounts;


namespace Service.Services.Interfaces
{
    public interface IAccountService
    {
        Task<RegisterResultDto> RegisterStudentAsync(RegisterDto dto);
        Task<LoginResultDto> LoginAsync(LoginDto dto);
        Task<List<StudentAccountDto>> GetStudentsAsync();
    }
}
