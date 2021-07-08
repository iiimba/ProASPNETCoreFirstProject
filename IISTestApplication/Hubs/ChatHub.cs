using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace IISTestApplication.Hubs
{
    public class ChatHub : Hub
    {
        public Task Receive(string message, string userName)
        {
            return Clients.All.SendAsync("Send", message, userName);
        }
    }
}
