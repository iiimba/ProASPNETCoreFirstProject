namespace RabbitMQWebApplication.Models
{
    public class RabbitMQDirectMessage : RabbitMQMessage
    {
        public RoutingKey RoutingKey { get; set; }
    }
}
