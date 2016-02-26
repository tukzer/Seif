namespace Seif.Soa
{
    public interface IRpcServer : IEndpoint
    {
        bool IsBound { get; }
    }
}