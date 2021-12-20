using RabbitMQReceiverApplication.Consumers;
using System;

namespace RabbitMQReceiverApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            //TaskConsumer.StartConsumer();

            LogsConsumer.StartConsumer();

            Console.ReadLine();
        }
    }
}
