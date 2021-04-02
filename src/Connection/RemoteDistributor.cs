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
    public class RemoteDistributor : IDistributor, IDisposable
    {
        // Client (WebInterface) Side

        public GrpcChannel channel;
        public Proto.Distributor.Distributor.DistributorClient client;

        public RemoteDistributor()
        {
            channel = GrpcChannel.ForAddress("https://localhost:5002");
            client = new Proto.Distributor.Distributor.DistributorClient(channel);
        }

        public async Task<Core.Route> GetRoute(Core.ConnectedGraph task)
        {
            Proto.Distributor.ConnectedGraph package = new Proto.Distributor.ConnectedGraph { Data = Serialiser.Serialise(task) };

            var result = await client.GetRouteAsync(package);

            return (Core.Route) Serialiser.Deserialise(result.Data);
        }

        public async Task<float> GetLowerBound(Core.ConnectedGraph task)
        {
            Proto.Distributor.ConnectedGraph package = new Proto.Distributor.ConnectedGraph { Data = Serialiser.Serialise(task) };

            var result = await client.GetLowerBoundAsync(package);

            return result.Lower;
        }

        public void Dispose()
        {
            channel.Dispose();
        }
    }
}
