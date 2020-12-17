using System;
using TaskManager.Core;

namespace TaskManager.Task
{
    [Serializable]
    public class RoutePackage
    {
        public AdjacencyMatrix matrix;
        public Node[] reference;

        public RoutePackage(AdjacencyMatrix matrix, Node[] reference)
        {
            this.matrix = matrix;
            this.reference = reference;
        }

        public RoutePackage(int size)
        {
            matrix = new AdjacencyMatrix(size);
            reference = new Node[size];
            for (int i = 0; i < size; i++)
            {
                reference[i] = new Node(i);
            }
        }

    }

}