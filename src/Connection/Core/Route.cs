using System;
using System.Text;


namespace Connection.Core
{
    [Serializable]
    public class Route
    {
        public Node[] nodes;

        public int Length { get => nodes.Length; }

        public Route(int size)
        {
            nodes = new Node[size];
        }

        public Route(Node[] nodes)
        {
            this.nodes = nodes;
        }

        public Node this[int a]
        {
            get => nodes[a];
            set => nodes[a] = value;
        }

        public void Flip(int a, int b)
        {
            Node tmp = nodes[a];
            nodes[a] = nodes[b];
            nodes[b] = tmp;
        }

        public (int a, int b) Flip()
        {
            int a = SelectRandom();
            int b = SelectRandom();
            Flip(a, b);
            return (a, b);
        }

        public void Shuffle()
        {
            for (int i = 0; i < 2 * nodes.Length; i++)
            {
                Flip();
            }
        }

        public int SelectRandom()
        {
            Random random = new Random();
            return random.Next(nodes.Length);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < nodes.Length; i++)
            {
                sb.Append(nodes[i].index + " ");
            }
            return sb.ToString();
        }


        public string Locations()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            for (int i = 0; i < nodes.Length - 1; i++)
            {
                sb.Append("[").Append(nodes[i].coord.longitude);
                sb.Append(",").Append(nodes[i].coord.latitude).Append("],");
            }
            sb.Append("[").Append(nodes[^1].coord.longitude);
            sb.Append(",").Append(nodes[^1].coord.latitude).Append("]");
            sb.Append("]");
            return sb.ToString();
        }

    }
}