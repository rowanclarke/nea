using System;
using Grpc.Net.Client;
using Distributor;
namespace Responder
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = new HelloRequest { Name = "Rowan" };
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Greeter.GreeterClient(channel);

            var reply = client.SayHello(input);

            Console.WriteLine(reply.Message);

            Console.ReadKey();
        }
    }
}
