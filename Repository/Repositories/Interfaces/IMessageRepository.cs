using Domain.Entities;

namespace Repository.Repositories.Interfaces
{
    public interface IMessageRepository
    {
        Task<IReadOnlyList<Message>> GetInboxAsync(string userId);
        Task<IReadOnlyList<Message>> GetThreadAsync(string userId, string otherId);
        Task AddAsync(Message message);
        Task MarkReadAsync(string receiverId, string senderId);
    }
}
