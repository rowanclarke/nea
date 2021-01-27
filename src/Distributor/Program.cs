using System;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Connection.Core;
using Connection;

namespace Distributor
{
    public class Program
    {
        static void Main(string[] args)
        {
            RoutePackage routePackage = new RoutePackage(100);
            RemoteWorker worker = new RemoteWorker();
            Route route = worker.GetRouteSubgraph(routePackage);
            Console.WriteLine(route.ToString());
            Console.ReadKey();
        }
    }
}
