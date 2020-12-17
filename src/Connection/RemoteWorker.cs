using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Core;
using TaskManager.Task;
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
        public TaskManager.Core.Route GetRouteSubgraph(TaskManager.Task.RoutePackage task)
        {
            // Client (Distributor) Side

            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Worker.WorkerClient(channel);

            BinaryFormatter formatter = new BinaryFormatter();

            MemoryStream serialStream = new MemoryStream();
            formatter.Serialize(serialStream, task);
            ByteString serial = ByteString.CopyFrom(serialStream.ToArray());
            serialStream.Close();
            RoutePackage package = new RoutePackage { Data = serial }; 
            
            var result = client.GetRouteSubgraph(package);

            MemoryStream deserialStream = new MemoryStream();
            result.Data.WriteTo(deserialStream);
            deserialStream.Position = 0;
            TaskManager.Core.Route route = (TaskManager.Core.Route) formatter.Deserialize(deserialStream);
            deserialStream.Close();
            return route;
        }
    }
}
