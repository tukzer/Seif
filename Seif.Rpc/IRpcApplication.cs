using System.Collections.Generic;
using System.Threading.Tasks;
using Seif.Rpc.Common;
using Seif.Rpc.Invoke;
using Seif.Rpc.Registry;

namespace Seif.Rpc
{
    public interface IRpcApplication
    {
        RpcConfiguration GlobalConfiguration { get; set; }

        Task RunAsync();
    }

    public class RpcConfiguration
    {
        public IServiceRegistry ServiceRegistry { get; set; }
        public IDictionary<string, IInvokeFilter> IncomingFilters { get; set; }
        public ITypeContainer TypeContainer { get; set; }
        public IDictionary<string, ISerializer> Serializers { get; set; }
    }

    //public class RegistryConfiguration
    //{

    //}

    //public class ProviderConfiguration
    //{
    //    public string Url { get; set; }
    //    public IDictionary<string, IInvokeFilter> ReceiveFilters { get; set; }

    //}

    //public class ConsumerConfiguration
    //{
    //    public IResultHandler ResultHandler { get; set; }
    //    public IDictionary<string, IInvokeFilter> SendFilters { get; set; }
    //}
}