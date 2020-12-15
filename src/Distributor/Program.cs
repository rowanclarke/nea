using System;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Connection;

namespace Distributor
{
    public class Program
    {
        static void Main(string[] args)
        {
            TaskManager.Task.RoutePackage routePackage = new TaskManager.Task.RoutePackage(100);
            RemoteWorker worker = new RemoteWorker();
            TaskManager.Core.Route route = worker.GetRouteSubgraph(routePackage);
            Console.ReadKey();
        }
    }
}
