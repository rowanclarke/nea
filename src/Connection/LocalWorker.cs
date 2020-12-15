using System;
using TaskManager.Task;
using TaskManager;

namespace Connection
{
    public class LocalWorker : IWorker
    {
        public TaskManager.Core.Route GetRouteSubgraph(TaskManager.Task.RoutePackage task)
        {
            // on all cores
            StochasticTask worker = new StochasticTask(task);
            worker.Run(1000);
            return worker.route;
        }
    }
}