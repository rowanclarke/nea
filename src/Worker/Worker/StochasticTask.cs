using System;
using Connection.Core;

namespace Worker
{
    public class StochasticTask
    {
        private AdjacencyMatrix matrix;
        private int size;
        public Route route;

        public double cost;
        public double min = double.PositiveInfinity;

        public StochasticTask(ConnectedGraph task)
        {
            matrix = task.matrix;
            route = new Route(task.nodes);
            route.Shuffle();
            size = route.nodes.Length;
        }

        public void Run(int iters)
        {
            int i = 0;
            while (i < iters)
            {
                (int a, int b) = route.Flip();
                CalculateCost();
                if (cost < min)
                {   
                    min = cost;
                    Console.WriteLine(min);
                    i = 0;
                }
                else
                {
                    route.Flip(a, b);
                    cost = min;
                }
                i++;
            }
        }

        public void CalculateCost()
        {
            cost = 0;
            for (int i = 0; i < size - 1; i++)
            {
                cost += matrix[route[i].index, route[i + 1].index];
            }
        }

    }

}