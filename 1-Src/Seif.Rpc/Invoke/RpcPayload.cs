using System.Collections.Generic;
using Seif.Rpc.Common;

namespace Seif.Rpc.Invoke
{
    public class RpcPayload
    {
        public IList<ParameterData> Parameters { get; set; }
        public IDictionary<string, string> Attributes { get; set; }
    }
}