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
using Proto = Connection.Proto;
using Connection.Core;
using Connection;

namespace Worker
{
    public class WorkerService : Proto.Worker.Worker.WorkerBase
    {
        private readonly ILogger<WorkerService> _logger;
        public WorkerService(ILogger<WorkerService> logger)
        {
            _logger = logger;
        }

        public override Task<Proto.Worker.Route> GetRouteSubgraph(Proto.Worker.RoutePackage request, ServerCallContext context)
        {
            // Server (Worker) Side

            LocalWorker localWorker = new LocalWorker();

            var route = localWorker.GetRouteSubgraph((RoutePackage) Serialiser.Deserialise(request.Data));

            var result = new Proto.Worker.Route { Data = Serialiser.Serialise(route) };

            return Task.FromResult(result);
        }

    }
}
