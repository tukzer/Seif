using System;
using System.Collections.Generic;

namespace Seif.Rpc.Invoke
{
    public interface IInvocation
    {
        string TraceId { get; set; }
        string ServiceName { get; }
        string MethodName { get; }
        Type ReturnType { get; }
        IList<string> Parameters { get; }
        IDictionary<string, string> Attributes { get; set; }
    }
}