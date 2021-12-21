using System;
using System.Collections.Concurrent;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMQRPC
{
    class RPCClient
    {
        public static void Call(uint number)
        {
            var respQueue = new BlockingCollection<string>();
            var factory = new ConnectionFactory() { HostName = "localhost" };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                var replyQueueName = channel.QueueDeclare().QueueName;
                var consumer = new EventingBasicConsumer(channel);

                var props = channel.CreateBasicProperties();
                var correlationId = Guid.NewGuid().ToString();
                props.CorrelationId = correlationId;
                props.ReplyTo = replyQueueName;

                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var response = Encoding.UTF8.GetString(body);
                    if (ea.BasicProperties.CorrelationId == correlationId)
                    {
                        respQueue.Add(response);
                    }
                };

                channel.BasicConsume(
                    consumer: consumer,
                    queue: replyQueueName,
                    autoAck: true);

                var messageBytes = Encoding.UTF8.GetBytes(number.ToString());
                channel.BasicPublish(
                    exchange: "",
                    routingKey: "rpc_queue",
                    basicProperties: props,
                    body: messageBytes);

                Console.WriteLine($"Got '{respQueue.Take()}'");

            }
        }
    }
}
