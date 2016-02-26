using System;
using Seif.Soa.Client;

namespace Seif.Soa
{
    public interface IInvoker
    {
        Type ServiceType { get; }

        InvokeResult Invoke(IInvocation invocation);
    }

    public interface IInvoker<T> : IInvoker where T : class 
    {
    }
}