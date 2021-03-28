using System;
using System.Text;

namespace Connection.Core
{
    [Serializable]
    public class RoutePackage
    {
        public AdjacencyMatrix matrix;
        public Route reference;

        public RoutePackage(AdjacencyMatrix matrix, Node[] reference)
        {
            this.matrix = matrix;
            this.reference = new Route(reference);
        }

        public RoutePackage(int size)
        {
            matrix = new AdjacencyMatrix(size);
            reference = new Route(size);
        }


    }

}