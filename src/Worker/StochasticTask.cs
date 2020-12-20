using System;
using Connection.Core;

namespace Worker
{
    public class StochasticTask
    {
        private AdjacencyMatrix matrix;
        private Node[] nodes;
        private int size;
        public Route route;
        
        public double cost = double.PositiveInfinity;

        public StochasticTask(RoutePackage task)
        {
            matrix = task.matrix;
            route = new Route(task.reference);
            nodes = task.reference;
            size = nodes.Length;
        }

        public void Run(int iters)
        {
            int i = 0;
            while (i < iters)
            {
                (int a, int b) c = route.Flip();
                calculateCost();
                if (route.cost < cost)
                {   
                    cost = route.cost;
                    Console.WriteLine(cost);
                    i = 0;
                }
                else
                {
                    route.Flip(c.a, c.b);
                    route.cost = cost;
                }
                i++;
            }
        }

        public void calculateCost()
        {
            route.cost = 0;
            for (int i = 0; i < size - 1; i++)
            {
                route.cost += matrix[route[i].index, route[i + 1].index];
            }
        }

    }

}