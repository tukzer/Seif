using System;

namespace Seif.Rpc.Invoke
{
    public class InvokeContext
    {
        public IInvocation Invocation { get; set; }
        public InvokeResult InvokeResult { get; set; }
        public Exception InvokeException { get; set; }
        public bool IsExceptionHandled { get; set; }
    }
}