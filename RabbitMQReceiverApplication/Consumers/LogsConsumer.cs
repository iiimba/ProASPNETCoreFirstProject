using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQWebApplication.Models;
using System;
using System.Linq;
using System.Text.Json;

namespace RabbitMQReceiverApplication.Consumers
{
    class LogsConsumer
    {
        private static int count;

        public static void StartConsumer()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: "logs", type: ExchangeType.Fanout);

                var queueName = channel.QueueDeclare().QueueName;
                channel.QueueBind(queue: queueName, exchange: "logs", routingKey: "");

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (sender, e) =>
                {
                    var body = e.Body.ToArray();
                    var message = JsonSerializer.Deserialize<Message>(body);

                    Console.WriteLine($"Received: {message} by consumer with tag: {consumer.ConsumerTags.First()}, handled message count: {++count}");
                };

                channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

                Console.ReadLine();
            }
        }
    }
}
