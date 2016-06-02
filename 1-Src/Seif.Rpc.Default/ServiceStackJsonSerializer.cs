using ServiceStack.Text;

namespace Seif.Rpc.Default
{
    public class ServiceStackJsonSerializer : ISerializer
    {
        public string Serialize<T>(T data)
        {
            return TypeSerializer.SerializeToString(data);
        }

        public T Deserialize<T>(string data) where T : class
        {
            return TypeSerializer.DeserializeFromString<T>(data);
        }
    }
}