using Connection;
using Core = Connection.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Distributor
{
    class LocalDistributor : IDistributor
    {
        public Core.Route GetRoute(Core.RoutePackage task)
        {
            Core.RoutePackage routePackage = new Core.RoutePackage(100);
            RemoteWorker worker = new RemoteWorker();
            return worker.GetRouteSubgraph(routePackage);
        }
    }
}
