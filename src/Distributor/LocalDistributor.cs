using Connection;
using Core = Connection.Core;

namespace Distributor
{
    class LocalDistributor : IDistributor
    {
        public Core.GeoRoute GetGeoRoute(Core.RoutePackage task)
        {
            RemoteWorker worker = new RemoteWorker();
            
            ORS.MatrixResponse matrixResponse = ORS.ORS.GetMatrix(task).Result;
            task.matrix = new Core.AdjacencyMatrix(matrixResponse.ToFloat());
            Core.Route route = worker.GetRouteSubgraph(task);

            ORS.DirectionResponse directionResponse = ORS.ORS.GetGeometry(route).Result;

            Core.GeoRoute geoRoute = new Core.GeoRoute(route, directionResponse.features[0].geometry.ToFloat());
            
            return geoRoute;
        }
    }
}
