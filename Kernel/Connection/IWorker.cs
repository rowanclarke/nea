using System;

namespace Connection
{
    public interface IWorker
    {
        public TaskManager.Core.Route GetRouteSubgraph(TaskManager.Task.RoutePackage task);
    }
}