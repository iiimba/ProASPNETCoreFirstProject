using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQWebApplication.Models;
using RabbitMQWebApplication.Services.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;

namespace RabbitMQWebApplication.Services
{
    public class RabbitMQService : IRabbitMQService
    {
        private readonly ILogger<RabbitMQService> _logger;
        private readonly Random _random = new Random();
        private readonly ConcurrentDictionary<ulong, string> _outstandingConfirms = new ConcurrentDictionary<ulong, string>();

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

        public void SendMessageToExchangeDirect(string message, RoutingKey routingKey)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: "direct_logs", type: ExchangeType.Direct);

                var body = JsonSerializer.SerializeToUtf8Bytes(new RabbitMQDirectMessage { Message = message, RoutingKey = routingKey });

                channel.BasicPublish(exchange: "direct_logs",
                    routingKey: routingKey.ToString().ToLower(),
                    basicProperties: null,
                    body: body);

                _logger.LogInformation($"Sent: {message}, routingKey(severity): {routingKey}");
            }
        }

        public void SendMessageToExchangeTopic(string message, string routingKey)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: "topic_logs", type: ExchangeType.Topic);

                var body = JsonSerializer.SerializeToUtf8Bytes(new RabbitMQTopicMessage { Message = message, RoutingKey = routingKey });

                channel.BasicPublish(exchange: "topic_logs",
                    routingKey: routingKey,
                    basicProperties: null,
                    body: body);

                _logger.LogInformation($"Sent: {message}, routingKey = {routingKey}");
            }
        }

        public void SendMessageUsingConfirmsFirstStrategy(string message)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ConfirmSelect();

                channel.QueueDeclare(queue: "confirms_queue",
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;

                var body = JsonSerializer.SerializeToUtf8Bytes(new Message { Text = message, Seconds = _random.Next(1, 5) });

                channel.BasicPublish(exchange: "",
                    routingKey: "confirms_queue",
                    basicProperties: properties,
                    body: body);

                channel.WaitForConfirmsOrDie(new TimeSpan(0, 0, 5));

                _logger.LogInformation($"Sent: {message}");
            }
        }

        public void SendMessageUsingConfirmsSecondBatchStrategy(string message)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ConfirmSelect();

                channel.QueueDeclare(queue: "confirms_queue",
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;

                var body = JsonSerializer.SerializeToUtf8Bytes(new Message { Text = message, Seconds = _random.Next(1, 5) });

                for (int i = 0; i < 100; i++)
                {
                    channel.BasicPublish(exchange: "",
                        routingKey: "confirms_queue",
                        basicProperties: properties,
                        body: body);
                }

                channel.WaitForConfirmsOrDie(new TimeSpan(0, 0, 5));

                _logger.LogInformation($"Sent: {message}");
            }
        }

        public void SendMessageUsingConfirmsThirdAsyncStrategy(string message)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ConfirmSelect();

                channel.QueueDeclare(queue: "confirms_queue",
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                channel.BasicAcks += (sender, ea) => CleanOutstandingConfirms(ea.DeliveryTag, ea.Multiple);

                channel.BasicNacks += (sender, ea) =>
                {
                    _outstandingConfirms.TryGetValue(ea.DeliveryTag, out string body);
                    _logger.LogError($"Message with body {body} has been nack-ed. Sequence number: {ea.DeliveryTag}, multiple: {ea.Multiple}");
                    CleanOutstandingConfirms(ea.DeliveryTag, ea.Multiple);
                };

                _outstandingConfirms.TryAdd(channel.NextPublishSeqNo, message);

                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;

                channel.BasicPublish(exchange: "",
                    routingKey: "confirms_queue",
                    basicProperties: properties,
                    body: Encoding.UTF8.GetBytes(message));

                if (!WaitUntil(60, () => _outstandingConfirms.IsEmpty))
                    throw new Exception("All messages could not be confirmed in 60 seconds");

                _logger.LogInformation($"Sent: {message}");
            }
        }

        private void CleanOutstandingConfirms(ulong sequenceNumber, bool multiple)
        {
            if (multiple)
            {
                var confirmed = _outstandingConfirms.Where(k => k.Key <= sequenceNumber);
                foreach (var entry in confirmed)
                {
                    _outstandingConfirms.TryRemove(entry.Key, out _);
                }
            }
            else
            {
                _outstandingConfirms.TryRemove(sequenceNumber, out _);
            }
        }

        private static bool WaitUntil(int numberOfSeconds, Func<bool> condition)
        {
            int waited = 0;
            while (!condition() && waited < numberOfSeconds * 1000)
            {
                Thread.Sleep(100);
                waited += 100;
            }

            return condition();
        }
    }
}
