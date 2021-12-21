using RabbitMQReceiverApplication.Consumers;
using RabbitMQWebApplication.Models;
using System;

namespace RabbitMQReceiverApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            //TaskConsumer.StartConsumer();

            //LogsConsumer.StartConsumer();

            //DirectLogsExample.StartConsumer(RoutingKey.Error);
            //DirectLogsExample.StartConsumer(new RoutingKey[] { RoutingKey.Info, RoutingKey.Warning, RoutingKey.Error });

            //TopicLogsExample.StartConsumer("*.info");
            //TopicLogsExample.StartConsumer("*.info", "*.error");
            //TopicLogsExample.StartConsumer("auth.#");
            //TopicLogsExample.StartConsumer("auth#");
            //TopicLogsExample.StartConsumer("auth.warning");

            RPCConsumer.StartConsumer();

            Console.ReadLine();
        }
    }
}
