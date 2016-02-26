using System;
using System.Collections.Generic;

namespace Seif.Soa.Client
{
    public interface IInvocation
    {
        string ServiceName { get; }
        string MethodName { get; }

        IDictionary<Type, object> Parameters { get; }
        IDictionary<string, string> Attributes { get; set; }
    }
}