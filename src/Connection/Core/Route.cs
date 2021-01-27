using System;
using System.Text;


namespace Connection.Core
{
    [Serializable]
    public class Route
    {
        public Node[] route;
        public double cost;
        

        public Route(int size)
        {
            route = new Node[size];
        }

        public Route(Node[] nodes)
        {
            route = new Node[nodes.Length];
            for (int i = 0; i < nodes.Length; i++)
            {
                route[i] = nodes[i];
            }
            Shuffle();
        }

        public Node this[int a]
        {
            get => route[a];
            set => route[a] = value;
        }

        public void Flip(int a, int b)
        {
            Node tmp = route[a];
            route[a] = route[b];
            route[b] = tmp;
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
            for (int i = 0; i < 2 * route.Length; i++)
            {
                Flip();
            }
        }

        public int SelectRandom()
        {
            Random random = new Random();
            return random.Next(route.Length);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < route.Length; i++)
            {
                sb.Append(route[i].index + " ");
            }
            sb.Append("\nCost: ").Append(cost);
            return sb.ToString();
        }
    }
}