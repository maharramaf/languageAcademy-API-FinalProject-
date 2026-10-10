using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace LanguageAcademy_API_FinalProject.Hubs
{
    [Authorize]
    public class NotificationHub : Hub
    {
    }
}
