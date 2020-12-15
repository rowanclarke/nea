using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Core;
using TaskManager.Task;
using Grpc.Core;
using Grpc.Net.Client;


namespace Connection
{
    public class RemoteWorker : IWorker
    {
        public TaskManager.Core.Route GetRouteSubgraph(TaskManager.Task.RoutePackage task)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Worker.WorkerClient(channel);

            // Serialise task
            var route = client.GetRouteSubgraph(serial);
            // Deserialise route
            return deserial;
        }
    }
}
