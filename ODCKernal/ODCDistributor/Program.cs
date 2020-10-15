using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using SocketManager;

namespace ODCDistributor
{
    
    class Distributor
    {
        static void Main(string[] args)
        {
            DistributorSocketManager dsm = new DistributorSocketManager(8080);
            Console.WriteLine("Listening...");
            Task h = Task.Run(dsm.Listen);
            h.Wait();
            Console.WriteLine("Connected.");
            Console.ReadKey();
        }
    }
}
