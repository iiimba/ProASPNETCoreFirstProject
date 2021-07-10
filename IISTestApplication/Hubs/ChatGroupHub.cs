using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace IISTestApplication.Hubs
{
    [Authorize]
    public class ChatGroupHub : Hub
    {
        string groupName = "groupName";

        public override async Task OnConnectedAsync()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task Send(string message, bool removeFromGroup)
        {
            if (removeFromGroup)
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
            }

            await Clients.Group(groupName).SendAsync("Receive", message, Context.User.Identity.Name);
        }
    }
}
