using System;
using System.Collections.Generic;
using Seif.Rpc.Common;

namespace Seif.Rpc.Invoke
{
    public class RpcInvocation : IInvocation
    {
        public virtual string ServiceUrl { get; set; }

        public virtual string TraceId { get; set; }

        public virtual ServiceKind ServiceKind { get; set; }

        public virtual string ServiceName { get; set; }

        public virtual string MethodName { get; set; }

        public virtual Type ReturnType { get; set; }

        public virtual IList<ParameterData> Parameters { get; set; }

        public virtual IDictionary<string, string> Attributes { get; set; }
    }
}