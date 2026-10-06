using Service.Helpers.DTOs.Reviews;


namespace Service.Services.Interfaces
{
    public interface IReviewService
    {
        Task<ReviewSectionDto?> GetUIAsync();
    }
}
