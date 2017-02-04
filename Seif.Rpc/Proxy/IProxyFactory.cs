using Seif.Rpc.Invoke;

namespace Seif.Rpc.Proxy
{
    public interface IProxyFactory
    {
        T CreateProxy<T>(IInvoker invoker, ProxyOptions options) where T : class;
    }
}