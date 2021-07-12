using Grpc.Net.Client;
using GrpcService;
using System;
using System.Threading.Tasks;

namespace GrpcClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var channel = GrpcChannel.ForAddress("http://localhost:5000");
            var client = new Greeter.GreeterClient(channel);
            var request = new HelloRequest { Name = "Vlad" };
            var response = await client.SayHelloAsync(request);

            Console.WriteLine(response);
        }
    }
}
