using System.Collections.Generic;
using Seif.Rpc.Dispatch;

namespace Seif.Rpc.Invoke
{
    public class ProxyOptions
    {
        public string EndpointUri { get; set; }
        public IDictionary<string,string> Attributes { get; set; }
        public ServiceKind ServiceKind { get; set; }

        public IDispathStragedy DispathStragedy { get; set; }
    }
}