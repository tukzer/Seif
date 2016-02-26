namespace Seif.Rpc.Client
{
    public interface IProxyFactory
    {
        T CreateInstance<T>();
    }
}