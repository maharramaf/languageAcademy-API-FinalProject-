using Service.Helpers.DTOs.Messages;

namespace Service.Services.Interfaces
{
    public interface IMessageService
    {
        Task<IReadOnlyList<ConversationDto>> GetInboxAsync(string userId);
        Task<IReadOnlyList<ContactDto>> GetContactsAsync(string userId);
        Task<ThreadDto?> GetThreadAsync(string userId, string otherId);
        Task<MessageResultDto> SendAsync(string userId, MessageCreateDto dto);
    }
}
