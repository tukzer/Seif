namespace Seif.Rpc.Dispatch
{
    public interface IDispatcher
    {
        ServiceMetta Select<T>();
    }
}