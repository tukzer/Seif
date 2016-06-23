using System;
using System.Collections.Generic;
using Seif.Rpc.Common;

namespace Seif.Rpc.Invoke
{
    public interface IInvocation
    {
        string TraceId { get; set; }
        ServiceKind ServiceKind { get; }
        string ServiceName { get; }
        string MethodName { get; }
        Type ReturnType { get; }

        IList<ParameterData> Parameters { get; }
        IDictionary<string, string> Attributes { get; set; }
    }
}