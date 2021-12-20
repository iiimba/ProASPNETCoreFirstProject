using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQWebApplication.Models;
using System;
using System.Linq;
using System.Text.Json;
using System.Threading;

namespace RabbitMQReceiverApplication
{
    class Program
    {
        private static int count;

        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "task_queue",
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                channel.BasicQos(0, 1, false);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (sender, e) =>
                {
                    var body = e.Body.ToArray();
                    var message = JsonSerializer.Deserialize<Message>(body);

                    Console.WriteLine($"Seconds to wait: {message.Seconds}");

                    Thread.Sleep(message.Seconds * 1000);

                    Console.WriteLine($"Received: {message} by consumer with tag: {consumer.ConsumerTags.First()}, handled message count: {++count}");

                    channel.BasicAck(e.DeliveryTag, false);
                };

                channel.BasicConsume(queue: "task_queue", autoAck: false, consumer: consumer);

                Console.ReadLine();
            }
        }
    }
}
