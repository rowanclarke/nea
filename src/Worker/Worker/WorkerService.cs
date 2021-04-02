using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using Proto = Connection.Proto;
using Connection.Core;
using Connection;

namespace Worker
{
    public class WorkerService : Proto.Worker.Worker.WorkerBase
    {
        // Server (Worker) Side

        private readonly ILogger<WorkerService> _logger;
        public WorkerService(ILogger<WorkerService> logger)
        {
            _logger = logger;
        }

        public override Task<Proto.Worker.Route> GetRouteSubgraph(Proto.Worker.ConnectedGraph request, ServerCallContext context)
        {
            LocalWorker localWorker = new LocalWorker();

            var route = localWorker.GetRouteSubgraph((ConnectedGraph) Serialiser.Deserialise(request.Data)).Result;

            var result = new Proto.Worker.Route { Data = Serialiser.Serialise(route) };

            return Task.FromResult(result);
        }

    }
}
