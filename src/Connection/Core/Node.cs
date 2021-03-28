using System;

namespace Connection.Core
{
    [Serializable]
    public enum Type
    {
        DISPATCH,
        CUSTOMER
    }

    [Serializable]
    public class Coordinate
    {
        public float longitude;
        public float latitude;

        public Coordinate(float longitude, float latitude)
        {
            this.longitude = longitude;
            this.latitude = latitude;
        }
    }

    [Serializable]
    public class Node
    {
        public int index;
        public Type type;
        public Coordinate coord;

        public Node(int index, Type type, Coordinate coord)
        {
            this.index = index;
            this.type = type;
            this.coord = coord;
        }

        public Node(int index, Coordinate coord)
        {
            this.index = index;
            this.coord = coord;
        }

        public Node(int index)
        {
            this.index = index;
        }
    }

}