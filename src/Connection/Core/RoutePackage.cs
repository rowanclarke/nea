using System;
using System.Text;

namespace Connection.Core
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

        public string Locations()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            for (int i = 0; i < reference.Length-1; i++)
            {
                sb.Append("[").Append(reference[i].coord.longitude);
                sb.Append(",").Append(reference[i].coord.latitude).Append("],");
            }
            sb.Append("[").Append(reference[reference.Length-1].coord.longitude);
            sb.Append(",").Append(reference[reference.Length - 1].coord.latitude).Append("]");
            sb.Append("]");
            return sb.ToString();
        }

    }

}