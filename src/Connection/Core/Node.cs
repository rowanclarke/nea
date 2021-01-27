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
        public double longitude;
        public double latitude;
    }

    [Serializable]
    public class Node
    {
        public int index;
        public string name;
        public Type type;
        public Coordinate coord;

        public Node(int index, string name, Type type, Coordinate coord)
        {
            this.index = index;
            this.name = name;
            this.type = type;
            this.coord = coord;
        }

        public Node(int index, string name)
        {
            this.index = index;
            this.name = name;
        }

        public Node(int index)
        {
            this.index = index;
        }
    }

}