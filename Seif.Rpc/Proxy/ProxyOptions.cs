using Seif.Rpc.Dispatch;
using Seif.Rpc.Invoke;
using Seif.Rpc.Registry;

namespace Seif.Rpc.Proxy
{
    public class ProxyOptions
    {
        public ProxyLifeCycle ProxyLifeCycle { get; set; }
        public IInvokeFilter[] InvokeFilters { get; set; }
        public ServiceMetta ServiceMetta { get; set; }
        public IDispatcher Dispatcher { get; set; }
        public IServiceRegistry ServiceRegistry { get; set; }
    }
}