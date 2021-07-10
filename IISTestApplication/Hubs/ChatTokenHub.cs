using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IISTestApplication.Hubs
{
    [Authorize(AuthenticationSchemes = "Identity.Application, Bearer")]
    public class ChatTokenHub : Hub
    {
        public async Task Send(string message)
        {
            await Clients.AllExcept(Context.ConnectionId).SendAsync("Receive", message, Context.User.Identity.Name);
        }

        [Authorize(AuthenticationSchemes = "Identity.Application, Bearer", Roles = "Admins")]
        public async Task Notify(string message)
        {
            await Clients.AllExcept(Context.ConnectionId).SendAsync("Receive", message, Context.User.Identity.Name);
        }
    }
}
