using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;

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
            _logger.LogInformation("Calculating Route");
            LocalWorker localWorker = new LocalWorker();
            // Deserialise request.Data
            TaskManager.Task.RoutePackage routePackage = new TaskManager.Task.RoutePackage(deserial);
            TaskManager.Core.Route route = localWorker.GetRouteSubgraph(routePackage);
            // Serialise route
            return serial;
        }

    }
}
