namespace Seif.Rpc
{
    public interface ISerializer
    {
        string Serialize<T>(T data) ;

        T Deserialize<T>(string data) where T : class;
    }
}