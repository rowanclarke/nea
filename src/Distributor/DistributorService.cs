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
        public DistributorService(ILogger<DistributorService> logger)
        {
            _logger = logger;
        }

        public override Task<Proto.GeoRoute> GetGeoRoute(Proto.RoutePackage request, ServerCallContext context)
        {
            LocalDistributor localWorker = new LocalDistributor();

            var route = localWorker.GetGeoRoute((Core.RoutePackage) Serialiser.Deserialise(request.Data));

            var result = new Proto.GeoRoute { Data = Serialiser.Serialise(route) };

            return Task.FromResult(result);
        }
    }
}
