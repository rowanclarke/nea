using Connection;
using System;
using System.Threading.Tasks;
using Core = Connection.Core;

namespace Distributor
{
    class LocalDistributor : IDistributor
    {
        public async Task<Core.Route> GetRoute(Core.ConnectedGraph task)
        {
            RemoteWorker worker = new RemoteWorker();
            Core.Route route = await worker.GetRouteSubgraph(task);
            worker.Dispose();
            return route;
        }

        public Task<float> GetLowerBound(Core.ConnectedGraph task)
        {
            LowerBoundTask lowerTask = new LowerBoundTask();
            float lower = lowerTask.LowerBound(task);
            Console.WriteLine($"Lower Bound: {lower}");
            return Task.FromResult(lower);
        }
    }
}
