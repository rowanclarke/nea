using Google.Protobuf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Connection
{
    public static class Serialiser
    {

        public static ByteString Serialise(object obj) {
            BinaryFormatter formatter = new BinaryFormatter();

            MemoryStream serialStream = new MemoryStream();
            formatter.Serialize(serialStream, obj);
            ByteString serial = ByteString.CopyFrom(serialStream.ToArray());
            serialStream.Close();

            return serial;
        }

        public static object Deserialise(ByteString serial) {
            BinaryFormatter formatter = new BinaryFormatter();

            MemoryStream deserialStream = new MemoryStream();
            serial.WriteTo(deserialStream);
            deserialStream.Position = 0;
            object obj = formatter.Deserialize(deserialStream);
            deserialStream.Close();

            return obj;
        }
    }
}
