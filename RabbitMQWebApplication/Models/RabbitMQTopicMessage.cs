namespace RabbitMQWebApplication.Models
{
    public class RabbitMQTopicMessage
    {
        public string Message { get; set; }

        public string RoutingKey { get; set; }
    }
}
