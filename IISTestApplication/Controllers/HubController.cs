using IISTestApplication.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace IISTestApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HubController : ControllerBase
    {
        private readonly IHubContext<ControllerHub> _hubContext;

        public HubController(IHubContext<ControllerHub> hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpPost]
        public async Task<IActionResult> Create(string product)
        {
            await _hubContext.Clients.All.SendAsync("Notify", $"Added: {product} - {DateTime.Now.ToShortTimeString()}");

            return Ok();
        }
    }
}
