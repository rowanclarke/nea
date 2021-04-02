using System;
using System.Collections.Generic;
using System.Text;

namespace Connection.Core
{
    [Serializable]
    public class Geometry
    {
        public float[,] geometry;

        public Geometry(float[,] geometry)
        {
            this.geometry = geometry;
        }
    }
}
