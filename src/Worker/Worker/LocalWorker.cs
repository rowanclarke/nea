using System;
using Connection;
using Connection.Core;

namespace Worker
{
    public class LocalWorker : IWorker
    {
        public Route GetRouteSubgraph(RoutePackage task)
        {
            StochasticTask worker = new StochasticTask(task);
            worker.Run(1000);
            return worker.route;
        }
    }
}