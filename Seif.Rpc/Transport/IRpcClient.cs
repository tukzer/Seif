namespace Seif.Rpc.Transport
{
    public interface IRpcClient : IEndpoint, IChannel
    {
        void Reconnect();
    }
}