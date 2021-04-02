using System;
using System.Threading.Tasks;
using Connection;
using Connection.Core;

namespace Worker
{
    public class LocalWorker : IWorker
    {
        public Task<Route> GetRouteSubgraph(ConnectedGraph task)
        {
            StochasticTask worker = new StochasticTask(task);
            worker.Run(1000);
            return Task.FromResult(worker.route);
        }
    }
}