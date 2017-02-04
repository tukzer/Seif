using Seif.Rpc.Invoke;

namespace Seif.Rpc.Proxy
{
    public class ProxyOptions
    {
        public ProxyLifeCycle ProxyLifeCycle { get; set; }
        public IInvokeFilter[] InvokeFilters { get; set; }
        public ServiceMetta ServiceMetta { get; set; }
    }
}