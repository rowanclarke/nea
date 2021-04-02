using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core = Connection.Core;
namespace Distributor
{
    public class LowerBoundTask
    {
        public float LowerBound(Core.ConnectedGraph task)
        {
            Core.ConnectedGraph subgraph = task.Subgraph(1, task.Length);
            float cost = MinimumSpanningTree(subgraph);
            float[] sorted = Sort(task, 0);
            return cost + sorted[0] + sorted[1];
        }

        public float[] Sort(Core.ConnectedGraph task, int node)
        {
            float[] sorted = new float[task.Length];
            for (int i = 0; i < sorted.Length; i++)
            {
                sorted[i] = task.matrix[node, i];
            }
            Array.Sort(sorted);
            return sorted;
        }

        public float MinimumSpanningTree(Core.ConnectedGraph task)
        {
            float sum = 0;
            int[] visited = new int[task.Length - 1];
            visited[0] = 0;
            for (int i = 1; i < task.Length - 1; i++) // n - 1 vertices
            {
                (int from, int to) min = (0, 0); // min from visited
                for (int j = 0; j < i; j++) // from
                {
                    for (int k = 1; k < task.Length - 1; k++) // to (unvisited)
                    {
                        if (visited.Contains(k)) continue;
                        if (min.to == 0) min = (j, k);
                        else if (task.matrix[min.from, min.to] > task.matrix[j, k]) min = (j, k);
                    }
                }
                sum += task.matrix[min.from, min.to];
                visited[i] = min.to; // add to visited
            }
            return sum;
        }
    }
}
