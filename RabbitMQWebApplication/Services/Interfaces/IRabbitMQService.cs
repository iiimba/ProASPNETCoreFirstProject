using RabbitMQWebApplication.Models;

namespace RabbitMQWebApplication.Services.Interfaces
{
    public interface IRabbitMQService
    {
        public void SendMessage(string message);

        void SendBatchMessages(string message, int count);

        public void SendMessageToExchange(string message);

        public void SendMessageToExchangeDirect(string message, RoutingKey routingKey);
    }
}
