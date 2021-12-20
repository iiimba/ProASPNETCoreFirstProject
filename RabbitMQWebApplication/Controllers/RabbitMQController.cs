using Microsoft.AspNetCore.Mvc;
using RabbitMQWebApplication.Models;
using RabbitMQWebApplication.Services.Interfaces;

namespace RabbitMQWebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RabbitMQController : ControllerBase
    {
        private readonly IRabbitMQService _rabbitMQService;

        public RabbitMQController(IRabbitMQService rabbitMQService)
        {
            _rabbitMQService = rabbitMQService;
        }

        [HttpPost("Send")]
        public IActionResult SendMessageToQueue(RabbitMQMessage message)
        {
            _rabbitMQService.SendMessage(message.Message);

            return Ok();
        }

        [HttpPost("SendBatch")]
        public IActionResult SendBatchMessagesToQueue(RabbitMQMessageBatch message)
        {
            _rabbitMQService.SendBatchMessages(message.Message, message.Count);

            return Ok();
        }
    }
}
