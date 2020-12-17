using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Google.Protobuf;

namespace Connection
{
    public class WorkerService : Worker.WorkerBase
    {
        private readonly ILogger<WorkerService> _logger;
        public WorkerService(ILogger<WorkerService> logger)
        {
            _logger = logger;
        }

        public override Task<Route> GetRouteSubgraph(RoutePackage request, ServerCallContext context)
        {
            // Server (Worker) Side

            LocalWorker localWorker = new LocalWorker();

            BinaryFormatter formatter = new BinaryFormatter();

            MemoryStream deserialStream = new MemoryStream();
            request.Data.WriteTo(deserialStream);
            deserialStream.Position = 0;
            TaskManager.Task.RoutePackage package = (TaskManager.Task.RoutePackage) formatter.Deserialize(deserialStream);
            deserialStream.Close();

            TaskManager.Core.Route route = localWorker.GetRouteSubgraph(package);

            MemoryStream serialStream = new MemoryStream();
            formatter.Serialize(serialStream, route);
            ByteString serial = ByteString.CopyFrom(serialStream.ToArray());
            serialStream.Close();
            Route result = new Route { Data = serial };

            return Task.FromResult(result);
        }

    }
}
