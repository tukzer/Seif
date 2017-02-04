using System.Collections.Generic;
using Seif.Rpc.Dispatch;
using Seif.Rpc.Invoke;
using Seif.Rpc.Proxy;

namespace Seif.Rpc.Runtime
{
    public interface IServiceClient : IServiceEndpoint
    {
        IChannel Protocol { get; set; }
        IDispatcher Dispatcher { get; set; }
        IProxyFactory ProxyFactory { get; set; }

        List<IInvokeFilter> Filters { get; }

        T CreateProxy<T>(ProxyOptions options);
    }

}