using System;
using System.Collections.Generic;
using Castle.DynamicProxy;
using Seif.Soa.Client;

namespace Seif.Rpc.Client
{
    public class CallingInterceptor : IInterceptor
    {
        public void Intercept(Castle.DynamicProxy.IInvocation invocation)
        {
            IDictionary<Type, object> parameters = new Dictionary<Type, object>();
            for (int i = 0; i < invocation.Arguments.Length; i++)
            {
                parameters.Add(invocation.Arguments[i].GetType(), invocation.Arguments[i]);
            }

            IInvocation rpcInvocation = new RpcInvocation(invocation.TargetType.Name,
                invocation.Method.Name, parameters);

            var invoker = InvokerPool.GetInvoker(invocation.TargetType);
            return invoker.Invoke(rpcInvocation);
        }
    }
}