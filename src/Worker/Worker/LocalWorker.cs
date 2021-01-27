using System;
using Connection.Core;

namespace Worker
{
    public class LocalWorker
    {
        public Route GetRouteSubgraph(RoutePackage task)
        {
            StochasticTask worker = new StochasticTask(task);
            worker.Run(1000);
            return worker.route;
        }
    }
}