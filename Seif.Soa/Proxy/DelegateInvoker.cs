using System;
using Seif.Soa.Client;

namespace Seif.Soa.Proxy
{
    public class DelegateInvoker<T> : IInvoker<T> where T: class 
    {
        private readonly T _instance;
        private readonly Func<T, IInvocation, InvokeResult> _invoker;

        protected DelegateInvoker(T instance, Func<T, IInvocation, InvokeResult> invoker)
        {
            _instance = instance;
            _invoker = invoker;
        }

        public Type ServiceType
        {
            get { return typeof (T); }
        }

        public InvokeResult Invoke(IInvocation invocation)
        {
            return _invoker.Invoke(_instance, invocation);
        }
    }
}