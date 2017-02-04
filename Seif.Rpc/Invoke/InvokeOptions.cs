using System.Collections.Generic;
using Seif.Rpc.Common;

namespace Seif.Rpc.Invoke
{
    public class InvokeOptions
    {
        public ISerializer Serializer { get; set; }
        public List<IInvokeFilter> Filters { get; set; }
        public IDictionary<string,string> Parameters { get; set; }
    }
}