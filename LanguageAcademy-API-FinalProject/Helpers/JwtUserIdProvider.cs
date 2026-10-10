using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.SignalR;

namespace LanguageAcademy_API_FinalProject.Helpers
{
    public class JwtUserIdProvider : IUserIdProvider
    {
        public string? GetUserId(HubConnectionContext connection)
        {
            return connection.User?.FindFirstValue(ClaimTypes.NameIdentifier)
                ?? connection.User?.FindFirstValue(JwtRegisteredClaimNames.Sub);
        }
    }
}
