using Proto = Connection.Proto.Distributor;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core = Connection.Core;
using Connection;

namespace Distributor
{
    class DistributorService : Proto.Distributor.DistributorBase
    {
        private readonly ILogger<DistributorService> _logger;
        private LocalDistributor localDistributor;

        public DistributorService(ILogger<DistributorService> logger)
        {
            localDistributor = new LocalDistributor();
            _logger = logger;
        }

        public override Task<Proto.Route> GetRoute(Proto.ConnectedGraph request, ServerCallContext context)
        {
            var route = localDistributor.GetRoute((Core.ConnectedGraph) Serialiser.Deserialise(request.Data)).Result;

            var result = new Proto.Route { Data = Serialiser.Serialise(route) };

            return Task.FromResult(result);
        }
        
        public override Task<Proto.LowerBound> GetLowerBound(Proto.ConnectedGraph request, ServerCallContext context)
        {
            Core.ConnectedGraph graph = (Core.ConnectedGraph) Serialiser.Deserialise(request.Data);

            var lower = localDistributor.GetLowerBound(graph).Result;

            var result = new Proto.LowerBound { Lower = lower };

            return Task.FromResult(result);
        }
    }
}
