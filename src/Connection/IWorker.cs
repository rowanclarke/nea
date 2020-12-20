using System;

namespace Connection
{
    public interface IWorker
    {
        public Core.Route GetRouteSubgraph(Core.RoutePackage task);
    }
}