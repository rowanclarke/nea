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
    public class RemoteDistributor : IDistributor
    {
        public Core.GeoRoute GetGeoRoute(Core.RoutePackage task)
        {
            // Client (WebInterface) Side

            var channel = GrpcChannel.ForAddress("https://localhost:5002");
            var client = new Proto.Distributor.Distributor.DistributorClient(channel);

            Proto.Distributor.RoutePackage package = new Proto.Distributor.RoutePackage { Data = Serialiser.Serialise(task) }; 
            
            var result = client.GetGeoRoute(package);

            return (Core.GeoRoute) Serialiser.Deserialise(result.Data);
        }
    }
}
