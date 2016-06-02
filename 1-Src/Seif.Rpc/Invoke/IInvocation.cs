using System;
using System.Collections.Generic;

namespace Seif.Rpc.Invoke
{
    public interface IInvocation
    {
        string TraceId { get; set; }
        ServiceKind ServiceKind { get; }
        string ServiceName { get; }
        string MethodName { get; }

        IDictionary<Type, object> Parameters { get; }
        IDictionary<string, string> Attributes { get; set; }
    }
}