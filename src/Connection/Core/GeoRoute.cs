using System;
using System.Collections.Generic;
using System.Text;

namespace Connection.Core
{
    [Serializable]
    public class GeoRoute
    {
        public Route route;
        public float[,] geometry;

        public GeoRoute(Route route, float[,] geometry)
        {
            this.route = route;
            this.geometry = geometry;
        }
    }
}
