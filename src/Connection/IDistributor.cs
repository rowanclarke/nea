using System;
using System.Threading.Tasks;

namespace Connection
{
    public interface IDistributor
    {
        public Task<Core.Route> GetRoute(Core.ConnectedGraph task);

        public Task<float> GetLowerBound(Core.ConnectedGraph task);
    }
}