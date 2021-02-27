using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Net.Client;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Google.Protobuf;

namespace Connection
{
    public class RemoteWorker : IWorker
    {
        public Core.Route GetRouteSubgraph(Core.RoutePackage task)
        {
            // Client (Distributor) Side

            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Proto.Worker.Worker.WorkerClient(channel);

            Proto.Worker.RoutePackage package = new Proto.Worker.RoutePackage { Data = Serialiser.Serialise(task) }; 
            
            var result = client.GetRouteSubgraph(package);
            
            return (Core.Route) Serialiser.Deserialise(result.Data);
        }
    }
}
