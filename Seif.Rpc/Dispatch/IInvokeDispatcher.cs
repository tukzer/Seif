using System;
using Seif.Rpc.Invoke;
using Seif.Rpc.Registry;

namespace Seif.Rpc.Dispatch
{
    public interface IInvokeDispatcher
    {
        IDispathStragedy Stragedy { get; set; }
        IInvoker Select<T>(DispatchOptions options);
    }

    public class DispatchOptions
    {
        public ServiceKind ServiceKind { get; set; }
    }
}