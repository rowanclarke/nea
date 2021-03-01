using System;
using System.Collections.Generic;

namespace Connection.Core
{
    [Serializable]
    public class AdjacencyMatrix
    {
        public double[,] matrix;

        public double this[int a, int b]
        {
            get => matrix[a, b];
            set => matrix[a, b] = value;
        }

        public AdjacencyMatrix(List<List<double>> lists) 
        {
            matrix = new double[lists.Count, lists[0].Count];
            
            for (int i = 0; i < lists.Count; i++)
            {
                for (int j = 0; j < lists[0].Count; j++)
                {
                    matrix[i, j] = lists[i][j];
                }
            }
        }

        public AdjacencyMatrix(double[,] matrix)
        {
            this.matrix = matrix;
        }

        public AdjacencyMatrix(int size)
        {
            matrix = new double[size, size];
            Randomise(size);
        }

        public void Randomise(int size)
        {
            Random rand = new Random();
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    matrix[i, j] = (float) rand.NextDouble();
                }
            }
        }
    }
}