using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace IISTestApplication.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        public async Task Send(string message)
        {
            await Clients.AllExcept(Context.ConnectionId).SendAsync("Receive", message, Context.User.Identity.Name);
        }

        [Authorize(Roles = "Admins")]
        public async Task Notify(string message)
        {
            await Clients.AllExcept(Context.ConnectionId).SendAsync("Receive", message, Context.User.Identity.Name);
        }
    }
}
