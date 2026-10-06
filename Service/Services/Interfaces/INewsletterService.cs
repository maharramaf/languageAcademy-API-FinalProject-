using Service.Helpers.DTOs.Newsletters;


namespace Service.Services.Interfaces
{
    public interface INewsletterService
    {
        Task<NewsletterSectionDto?> GetUIAsync();
        Task<bool> SubscribeAsync(string email);
    }
}
