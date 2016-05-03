using System;
using System.Linq;

namespace Seif.Rpc.Invoke
{
    public class InvokerWrapper : IInvoker
    {
        private readonly IInvoker _invoker;
        private readonly IInvokeFilter[] _filters;

        public InvokerWrapper(IInvoker invoker, IInvokeFilter[]  filters)
        {
            _invoker = invoker;
            _filters = filters;
        }

        public string Url
        {
            get { return _invoker == null ? string.Empty : _invoker.Url; }
        }

        public InvokeResult Invoke(IInvocation invocation)
        {
            foreach (var invokeFilter in _filters)
            {
                invocation = invokeFilter.PreInvoke(invocation);
            }

            var result = _invoker.Invoke(invocation);

            // Reverse
            foreach (var invokeFilter in _filters.Reverse())
            {
                result = invokeFilter.AfterInvoke(result);
            }

            return result;
        }
    }
}