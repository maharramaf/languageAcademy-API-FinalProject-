using Service.Helpers.DTOs.Certificates;

namespace Service.Services.Interfaces
{
    public interface ICertificateService
    {
        Task<IReadOnlyList<CertificateDto>> GetMineAsync(string studentId);
        Task<CertificateDto?> GetBySlugAsync(string studentId, string slug);
        Task<CertificateDto?> TryIssueAsync(string studentId, int courseId);
    }
}
