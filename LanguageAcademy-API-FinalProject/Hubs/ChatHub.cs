using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Service.Helpers.DTOs.Messages;
using Service.Services.Interfaces;

namespace LanguageAcademy_API_FinalProject.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly IMessageService _messageService;

        public ChatHub(IMessageService messageService)
        {
            _messageService = messageService;
        }

        public async Task Send(string receiverId, string body)
        {
            var userId = Context.UserIdentifier;
            if (string.IsNullOrWhiteSpace(userId))
                throw new HubException("Unauthorized.");

            var result = await _messageService.SendAsync(userId, new MessageCreateDto
            {
                ReceiverId = receiverId,
                Body = body
            });
            if (!result.Succeeded || result.Message is null)
                throw new HubException(result.Errors.FirstOrDefault() ?? "Could not send the message.");

            await Clients.Users(userId, result.Message.ReceiverId)
                .SendAsync("ReceiveMessage", result.Message);
        }
    }
}
