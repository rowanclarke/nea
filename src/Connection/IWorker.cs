using System;
using System.Threading.Tasks;

namespace Connection
{
    public interface IWorker
    {
        public Task<Core.Route> GetRouteSubgraph(Core.ConnectedGraph task);
    }
}