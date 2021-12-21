using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQWebApplication.Models;
using System;
using System.Text.Json;

namespace RabbitMQReceiverApplication.Consumers
{
    class TopicLogsExample
    {
        public static void StartConsumer(params string[] routingKeys)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: "topic_logs", type: ExchangeType.Topic);

                var queueName = channel.QueueDeclare().QueueName;

                foreach (var routingKey in routingKeys)
                {
                    channel.QueueBind(queue: queueName, exchange: "topic_logs", routingKey: routingKey);
                }

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (sender, e) =>
                {
                    var body = e.Body.ToArray();
                    var message = JsonSerializer.Deserialize<RabbitMQTopicMessage>(body);

                    Console.WriteLine($"Received: {message.Message}, routingKey = {message.RoutingKey}");
                };

                channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

                Console.ReadLine();
            }
        }
    }
}
