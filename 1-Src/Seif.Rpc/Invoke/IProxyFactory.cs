using System;

namespace Seif.Rpc.Invoke
{
    public interface IProxyFactory
    {
        T CreateProxy<T>(ProxyOptions options, IInvokeFilter[] filters) where T : class ; 
    }
}