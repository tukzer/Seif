using System;
using System.Collections.Generic;

namespace Seif.Rpc.Invoke
{
    public interface IInvocation
    {
        string TraceId { get; set; }
        string ServiceName { get; set; }
        string MethodName { get; set; }
        IList<object> Parameters { get; set; }
        IDictionary<string, string> Attributes { get; set; }
    }
}