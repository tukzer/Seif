namespace Seif.Rpc.Transport
{
    public interface IChannel : IEndpoint
    {
        bool IsConnected { get;  }
    }
}