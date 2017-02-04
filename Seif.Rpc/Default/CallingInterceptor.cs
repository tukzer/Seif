using System;
using System.Collections.Generic;
using System.Linq;
using Castle.DynamicProxy;
using Seif.Rpc.Common;
using Seif.Rpc.Invoke;
using IInvocation = Seif.Rpc.Invoke.IInvocation;

namespace Seif.Rpc.Default
{
    public class CallingInterceptor<T> : IInterceptor
    {
        private readonly IInvoker _invoker;
        private readonly IInvokeFilter[] _filters;

        //public CallingInterceptor(IInvokeDispatcher dispatcher, IInvokeFilter[] filters)
        //{
        //    _dispatcher = dispatcher;
        //    _filters = filters;
        //}

        public CallingInterceptor(IInvoker invoker, IInvokeFilter[] filters)
        {
            _invoker = invoker;
            _filters = filters;
        }

        public void Intercept(Castle.DynamicProxy.IInvocation invocation)
        {
            var wrapper = new InvokerWrapper(_invoker, _filters);

            var serviceType = typeof (T);
            IInvocation rpcInvocation = new RpcInvocation
            {
                TraceId = Guid.NewGuid().ToString("N"),
                ServiceName = serviceType.FullName,
                MethodName = invocation.Method.Name,
                Parameters = invocation.Arguments.ToList(),
                Attributes = new Dictionary<string, string>()
            };

            var result = wrapper.Invoke(rpcInvocation);
            invocation.ReturnValue = result;
        }
    }
}