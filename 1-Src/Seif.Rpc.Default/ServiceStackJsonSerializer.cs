using System;
using ServiceStack.Text;

namespace Seif.Rpc.Default
{
    public class ServiceStackJsonSerializer : ISerializer
    {
        public string Serialize<T>(T data)
        {
            return TypeSerializer.SerializeToString(data);
        }

        public string Serialize(Type type, object data)
        {
            return TypeSerializer.SerializeToString(data, type);
        }

        public T Deserialize<T>(string data) where T : class
        {
            return TypeSerializer.DeserializeFromString<T>(data);
        }

        public object Deserialize(Type type, string data)
        {
            return TypeSerializer.DeserializeFromString(data, type);
        }
    }
}