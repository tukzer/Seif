using System;
using System.Collections.Concurrent;

namespace Seif.Soa.Client
{
    public class InvokerPool
    {
        public static ConcurrentDictionary<Type, IInvoker> _invokers;

        public static ConcurrentDictionary<Type, IInvoker> Invokers
        {
            get { return _invokers; }
        }

        public static IInvoker GetInvoker(Type type)
        {
            if (_invokers != null && _invokers.ContainsKey(type))
            {
                return _invokers[type];
            }

            return null;
        }
    }
}