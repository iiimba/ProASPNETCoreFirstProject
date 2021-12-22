using RabbitMQWebApplication.Models;

namespace RabbitMQWebApplication.Services.Interfaces
{
    public interface IRabbitMQService
    {
        void SendMessage(string message);

        void SendBatchMessages(string message, int count);

        void SendMessageToExchange(string message);

        void SendMessageToExchangeDirect(string message, RoutingKey routingKey);

        void SendMessageToExchangeTopic(string message, string routingKey);

        void SendMessageUsingConfirmsFirstStrategy(string message);

        void SendMessageUsingConfirmsSecondBatchStrategy(string message);

        void SendMessageUsingConfirmsThirdAsyncStrategy(string message);
    }
}
