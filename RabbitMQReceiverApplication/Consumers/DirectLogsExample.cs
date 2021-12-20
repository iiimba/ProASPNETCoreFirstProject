using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQWebApplication.Models;
using System;
using System.Text.Json;

namespace RabbitMQReceiverApplication.Consumers
{
    class DirectLogsExample
    {
        public static void StartConsumer(params RoutingKey[] routingKeys)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: "direct_logs", type: ExchangeType.Direct);

                var queueName = channel.QueueDeclare().QueueName;

                foreach (var routingKey in routingKeys)
                {
                    channel.QueueBind(queue: queueName, exchange: "direct_logs", routingKey: routingKey.ToString().ToLower());
                }
                
                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (sender, e) =>
                {
                    var body = e.Body.ToArray();
                    var message = JsonSerializer.Deserialize<RabbitMQDirectMessage>(body);

                    Console.WriteLine($"Received: {message.Message}, routingKey: {message.RoutingKey}");
                };

                channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

                Console.ReadLine();
            }
        }
    }
}
