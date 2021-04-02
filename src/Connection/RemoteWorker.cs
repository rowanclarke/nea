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
    public class RemoteWorker : IWorker, IDisposable
    {
        // Client (Distributor) Side

        public GrpcChannel channel;
        public Proto.Worker.Worker.WorkerClient client;

        public RemoteWorker()
        {
            channel = GrpcChannel.ForAddress("https://localhost:5001");
            client = new Proto.Worker.Worker.WorkerClient(channel);
        }

        public async Task<Core.Route> GetRouteSubgraph(Core.ConnectedGraph task)
        {
            Proto.Worker.ConnectedGraph package = new Proto.Worker.ConnectedGraph { Data = Serialiser.Serialise(task) }; 
            
            var result = await client.GetRouteSubgraphAsync(package);
            
            return (Core.Route) Serialiser.Deserialise(result.Data);
        }

        public void Dispose()
        {
            channel.Dispose();
        }
    }
}
