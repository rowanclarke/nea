using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Connection;
using Core = Connection.Core;
using WebInterface;

namespace WebInterface
{
    public class WebService : IWeb
    {
        public async Task<Core.AdjacencyMatrix> GetAdjacencyMatrix(Core.Route route)
        {
            Console.WriteLine("GetAdjacencyMatrix WebService");

            Console.WriteLine(route.ToString());

            var response = await ORS.GetAdjacencyMatrixAsync(route);
            
            return new Core.AdjacencyMatrix(response.ToFloat());
        }

        public async Task<float> GetLowerBound(Core.ConnectedGraph task)
        {
            RemoteDistributor distributor = new RemoteDistributor();

            float lower = await distributor.GetLowerBound(task);

            distributor.Dispose();

            return lower;
        }

        public async Task<Core.Route> GetRoute(Core.ConnectedGraph task)
        {
            RemoteDistributor distributor = new RemoteDistributor();

            Core.Route route = await distributor.GetRoute(task);

            distributor.Dispose();

            return route;
        }

        public async Task<Core.Geometry> GetGeometry(Core.Route route)
        {
            DirectionResponse response = await ORS.GetGeometryAsync(route);

            return new Core.Geometry(response.features[0].geometry.ToFloat());
        }
    }
}
