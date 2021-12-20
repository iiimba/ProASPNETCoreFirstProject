using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQWebApplication.Models;
using RabbitMQWebApplication.Services.Interfaces;
using System;
using System.Text.Json;

namespace RabbitMQWebApplication.Services
{
    public class RabbitMQService : IRabbitMQService
    {
        private readonly ILogger<RabbitMQService> _logger;
        private readonly Random _random = new Random();

        public RabbitMQService(ILogger<RabbitMQService> logger)
        {
            _logger = logger;
        }

        public void SendMessage(string message)
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

                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;

                var body = JsonSerializer.SerializeToUtf8Bytes(new Message { Text = message, Seconds = _random.Next(1, 5) });

                channel.BasicPublish(exchange: "", 
                    routingKey: "task_queue",
                    basicProperties: properties,
                    body: body);

                _logger.LogInformation($"Sent: {message}");
            }
        }

        public void SendBatchMessages(string message, int count)
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

                var batch = channel.CreateBasicPublishBatch();

                for (int i = 0; i < count; i++)
                {
                    var body = JsonSerializer.SerializeToUtf8Bytes(new Message { Text = message, Seconds = _random.Next(1, 5) });

                    var properties = channel.CreateBasicProperties();
                    properties.Persistent = true;

                    batch.Add(exchange: "",
                        routingKey: "task_queue",
                        mandatory: false,
                        properties: properties,
                        body: (ReadOnlyMemory<byte>)body);
                }

                batch.Publish();

                _logger.LogInformation($"Sent batch");
            }
        }

        public void SendMessageToExchange(string message)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: "logs", type: ExchangeType.Fanout);

                var body = JsonSerializer.SerializeToUtf8Bytes(new Message { Text = message, Seconds = _random.Next(1, 5) });

                channel.BasicPublish(exchange: "logs",
                    routingKey: "",
                    basicProperties: null,
                    body: body);

                _logger.LogInformation($"Sent: {message}");
            }
        }
    }
}
