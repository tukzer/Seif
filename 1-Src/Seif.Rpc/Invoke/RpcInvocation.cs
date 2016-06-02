using System;
using System.Collections.Generic;

namespace Seif.Rpc.Invoke
{
    public class RpcInvocation : IInvocation
    {
        public virtual string ServiceUrl { get; set; }

        public virtual string TraceId { get; set; }

        public virtual ServiceKind ServiceKind { get; set; }

        public virtual string ServiceName { get; set; }

        public virtual string MethodName { get; set; }

        public virtual IDictionary<Type, object> Parameters { get; set; }

        public virtual IDictionary<string, string> Attributes { get; set; }
    }
}