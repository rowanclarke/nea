using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Connection
{
    public interface IWeb
    {
        public Task<Core.AdjacencyMatrix> GetAdjacencyMatrix(Core.Route nodes);

        public Task<float> GetLowerBound(Core.ConnectedGraph graph);

        public Task<Core.Route> GetRoute(Core.ConnectedGraph task);

        public Task<Core.Geometry> GetGeometry(Core.Route nodes);
    }
}
