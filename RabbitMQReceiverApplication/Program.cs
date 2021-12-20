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

            DirectLogsExample.StartConsumer(RoutingKey.Error);

            //DirectLogsExample.StartConsumer(new RoutingKey[] { RoutingKey.Info, RoutingKey.Warning, RoutingKey.Error });

            Console.ReadLine();
        }
    }
}
