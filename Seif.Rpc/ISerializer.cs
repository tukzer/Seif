namespace Seif.Rpc
{
    public interface ISerializer
    {
        string Name { get; set; }
        string Serialize<T>(T data);

        T Deserialize<T>(string data);
    }
}