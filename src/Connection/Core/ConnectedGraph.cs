using System;
using System.Text;

namespace Connection.Core
{
    [Serializable]
    public class ConnectedGraph : Route
    {
        public AdjacencyMatrix matrix;

        public ConnectedGraph(AdjacencyMatrix matrix, Node[] reference) : base(reference)
        {
            this.matrix = matrix;
        }

        public ConnectedGraph(int size) : base(size)
        {
            matrix = new AdjacencyMatrix(size);
        }

        public ConnectedGraph Subgraph(int from, int to)
        {
            ConnectedGraph package = new ConnectedGraph(to - from);
            int a = 0;
            for (int i = from; i < to; i++)
            {
                package.nodes[a] = nodes[i];

                int b = 0;
                for (int j = from; j < to; j++)
                {
                    package.matrix[a, b] = matrix[i, j];
                    b++;
                }
                a++;
            }
            return this;
        }
    }
}