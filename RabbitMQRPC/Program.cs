using System;

namespace RabbitMQRPC
{
    class Program
    {
        static void Main(string[] args)
        {
            RPCClient.Call(20);
            RPCClient.Call(30);
        }
    }
}
