using System;
using TaskManager.Core;

namespace TaskManager.Task
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
            Console.WriteLine(route.ToString());
        }

        public void Run(int iters)
        {
            for (int i = 0; i < iters; i++)
            {
                (int a, int b) c = route.Flip();
                calculateCost();                
                //Console.WriteLine($"{c.a}, {c.b} -> {route.cost}  \t  \t({cost})");
                if (route.cost < cost)
                {
                    //Console.WriteLine(route.cost + " < " + cost);
                    //Console.WriteLine(route.ToString());
                    cost = route.cost;
                }
                else
                {
                    route.Flip(c.a, c.b);
                }
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