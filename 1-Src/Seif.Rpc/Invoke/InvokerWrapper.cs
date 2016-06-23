using System;
using System.Linq;

namespace Seif.Rpc.Invoke
{
    public class InvokerWrapper : IInvoker
    {
        private readonly IInvoker _invoker;
        private readonly IInvokeFilter[] _filters;
        private readonly ResultHandler _resultHandler;

        public InvokerWrapper(IInvoker invoker, IInvokeFilter[]  filters, ResultHandler resultHandler = null)
        {
            _invoker = invoker;
            _filters = filters;
            _resultHandler = resultHandler ?? SeifApplication.AppEnv.GlobalConfiguration.ConsumerConfiguration.GetResultHandler();
        }

        public string Url
        {
            get { return _invoker == null ? string.Empty : _invoker.Url; }
        }

        public ISerializer Serializer
        {
            get
            {
                if (_invoker != null)
                    return _invoker.Serializer;
                return null;
            }
        }

        public ResultHandler ResultHandler
        {
            get { return _resultHandler; }
        }

        public InvokeResult Invoke(IInvocation invocation)
        {
            foreach (var invokeFilter in _filters)
            {
                invocation = invokeFilter.PreInvoke(invocation);
            }

            var result = _invoker.Invoke(invocation);
            result.Result = ResultHandler.ProcessResult(result, invocation.ReturnType, _invoker.Serializer);

            // Reverse
            foreach (var invokeFilter in _filters.Reverse())
            {
                result = invokeFilter.PostInvoke(result);
            }

            return result;
        }
    }
}