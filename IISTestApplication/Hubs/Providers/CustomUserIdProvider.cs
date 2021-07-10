using Microsoft.AspNetCore.SignalR;

namespace IISTestApplication.Hubs.Providers
{
    public class CustomUserIdProvider : IUserIdProvider
    {
        public string GetUserId(HubConnectionContext connection)
        {
            return connection.User?.Identity.Name;
            // return connection.User?.FindFirst(ClaimTypes.Name)?.Value;
        }
    }
}
