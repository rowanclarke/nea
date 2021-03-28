using System;
using System.Collections.Generic;

namespace Connection.Core
{
    [Serializable]
    public class AdjacencyMatrix
    {
        public float[,] matrix;

        public float this[int a, int b]
        {
            get => matrix[a, b];
            set => matrix[a, b] = value;
        }

        public AdjacencyMatrix(float[,] matrix)
        {
            this.matrix = matrix;
        }

        public AdjacencyMatrix(int size)
        {
            matrix = new float[size, size];
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