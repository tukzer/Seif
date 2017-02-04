using System;
using System.Collections.Generic;

namespace Seif.Rpc.Invoke
{
    public class RpcInvocation : IInvocation
    {

        public string TraceId { get;set; }

        public string ServiceName { get; set; }

        public string MethodName { get; set; }


        public IList<object> Parameters { get; set; }

        public IDictionary<string, string> Attributes { get; set; }
    }
}