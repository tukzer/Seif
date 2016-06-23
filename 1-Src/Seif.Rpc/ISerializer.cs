using System;

namespace Seif.Rpc
{
    public interface ISerializer
    {
        string Serialize<T>(T data) ;

        string Serialize(Type type, object data);

        T Deserialize<T>(string data) where T : class;

        object Deserialize(Type type, string data);
    }
}