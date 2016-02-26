using System;
using System.Collections.Generic;
using Castle.DynamicProxy;
using Seif.Soa.Proxy;
using IInvocation = Seif.Soa.Client.IInvocation;

namespace Seif.Soa.Default
{
    public class CallingInterceptor<T> : IInterceptor where T : class 
    {
        private readonly IInvoker<T> _invoker;
        private readonly string _url;

        public CallingInterceptor(IInvoker<T> invoker, string url)
        {
            _invoker = invoker;
            _url = url;
        }

        public void Intercept(Castle.DynamicProxy.IInvocation invocation)
        {
            IDictionary<Type, object> parameters = new Dictionary<Type, object>();
            for (int i = 0; i < invocation.Arguments.Length; i++)
            {
                parameters.Add(invocation.Arguments[i].GetType(), invocation.Arguments[i]);
            }

            IInvocation rpcInvocation = new RpcInvocation(_url,invocation.TargetType.Name,
                invocation.Method.Name, parameters);

            invocation.ReturnValue = _invoker.Invoke(rpcInvocation);
        }
    }
}