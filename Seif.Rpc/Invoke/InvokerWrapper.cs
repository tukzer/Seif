using System;
using System.Linq;

namespace Seif.Rpc.Invoke
{
    public class InvokerWrapper 
    {
        private readonly IInvoker _invoker;
        private readonly IInvokeFilter[] _filters;

        public InvokerWrapper(IInvoker invoker, IInvokeFilter[] filters)
        {
            _filters = filters;
            _invoker = invoker;
        }

        //private readonly ResultHandler _resultHandler;

        //public InvokerWrapper(IInvoker invoker, IInvokeFilter[]  filters, ResultHandler resultHandler = null)
        //{
        //    _invoker = invoker;
        //    _filters = filters;
        //    _resultHandler = resultHandler ?? SeifApplication.AppEnv.GlobalConfiguration.ConsumerConfiguration.GetResultHandler();
        //}

        //public string Url
        //{
        //    get { return _invoker == null ? string.Empty : _invoker.Url; }
        //}

        //public ISerializer Serializer
        //{
        //    get
        //    {
        //        if (_invoker != null)
        //            return _invoker.Serializer;
        //        return null;
        //    }
        //}

        //public ResultHandler ResultHandler
        //{
        //    get { return _resultHandler; }
        //}

        public InvokeResult Invoke(IInvocation invocation)
        {
            var context = new InvokeContext {Invocation = invocation};

            foreach (var invokeFilter in _filters.Where(p => p is IPreInvokeFilter))
            {
                var preFilter = invokeFilter as IPreInvokeFilter;
                if (preFilter != null) preFilter.Execute(context);
            }

            try
            {
                var result  = _invoker.Call(invocation);
                result.Status = ResultStatus.Success;
                context.InvokeResult = result;

                // Reverse
                var postFilters = _filters.Where(p => p is IPostInvokeFilter).Cast<IPostInvokeFilter>();
                foreach (var invokeFilter in postFilters.Reverse())
                {
                    invokeFilter.Execute(context);
                }

                return context.InvokeResult;
            }
            catch (Exception ex)
            {
                context.InvokeException = ex;

                // Reverse
                var errorFilters = _filters.Where(p => p is IExceptionFilter).Cast<IExceptionFilter>();
                foreach (var invokeFilter in errorFilters.Reverse())
                {
                    invokeFilter.Execute(context);
                    if (context.IsExceptionHandled) return context.InvokeResult;
                }
                
                throw context.InvokeException;
            }
            //result.Result = ResultHandler.ProcessResult(result, invocation.ReturnType, _invoker.Serializer);

            //// Reverse
            //foreach (var invokeFilter in _filters.Reverse())
            //{
            //    result = invokeFilter.PostInvoke(result);
            //}
         
            //return result;
        }
    }
}