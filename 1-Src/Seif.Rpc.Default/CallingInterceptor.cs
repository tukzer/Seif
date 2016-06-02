using System;
using System.Collections.Generic;
using Castle.DynamicProxy;

namespace Seif.Rpc.Invoke.Default
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

            IDictionary<Type, object> parameters = new Dictionary<Type, object>();
            for (int i = 0; i < invocation.Arguments.Length; i++)
            {
                parameters.Add(invocation.Arguments[i].GetType(), invocation.Arguments[i]);
            }

            var serviceType = typeof (T);
            IInvocation rpcInvocation = new RpcInvocation
            {
                TraceId = Guid.NewGuid().ToString("N"),
                ServiceName = serviceType.FullName,
                MethodName = invocation.Method.Name,
                Parameters = parameters,
                Attributes = new Dictionary<string, string>()
            };

            invocation.ReturnValue = wrapper.Invoke(rpcInvocation);
        }
    }
}