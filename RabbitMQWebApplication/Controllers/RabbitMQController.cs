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

        [HttpPost("SendMessageToQueue")]
        public IActionResult SendMessageToQueue(RabbitMQMessage message)
        {
            _rabbitMQService.SendMessage(message.Message);

            return Ok();
        }

        [HttpPost("SendBatchMessagesToQueue")]
        public IActionResult SendBatchMessagesToQueue(RabbitMQMessageBatch message)
        {
            _rabbitMQService.SendBatchMessages(message.Message, message.Count);

            return Ok();
        }

        [HttpPost("SendMessageToExchange")]
        public IActionResult SendMessageToExchange(RabbitMQMessage message)
        {
            _rabbitMQService.SendMessageToExchange(message.Message);
            
            return Ok();
        }

        [HttpPost("SendMessageToExchangeDirect")]
        public IActionResult SendMessageToExchangeDirect(RabbitMQDirectMessage message)
        {
            _rabbitMQService.SendMessageToExchangeDirect(message.Message, message.RoutingKey);
            
            return Ok();
        }

        [HttpPost("SendMessageToExchangeTopic")]
        public IActionResult SendMessageToExchangeTopic(RabbitMQTopicMessage message)
        {
            _rabbitMQService.SendMessageToExchangeTopic(message.Message, message.RoutingKey);

            return Ok();
        }

        [HttpPost("SendMessageUsingConfirmsFirstStrategy")]
        public IActionResult SendMessageUsingConfirmsFirstStrategy(RabbitMQMessage message)
        {
            _rabbitMQService.SendMessageUsingConfirmsFirstStrategy(message.Message);

            return Ok();
        }

        [HttpPost("SendMessageUsingConfirmsSecondBatchStrategy")]
        public IActionResult SendMessageUsingConfirmsSecondBatchStrategy(RabbitMQMessage message)
        {
            _rabbitMQService.SendMessageUsingConfirmsSecondBatchStrategy(message.Message);

            return Ok();
        }

        [HttpPost("SendMessageUsingConfirmsThirdAsyncStrategy")]
        public IActionResult SendMessageUsingConfirmsThirdAsyncStrategy(RabbitMQMessage message)
        {
            _rabbitMQService.SendMessageUsingConfirmsThirdAsyncStrategy(message.Message);

            return Ok();
        }

        [HttpPost("SendToAlternativeExchange")]
        public IActionResult SendToAlternativeExchange(RabbitMQMessage message)
        {
            _rabbitMQService.SendToAlternativeExchange(message.Message);

            return Ok();
        }
    }
}
