using System;
using System.Collections.Concurrent;
using Seif.Rpc.Invoke;
using Seif.Rpc.Registry;

namespace Seif.Rpc.Dispatch
{
    public class RoundRobinDispatchStragedy : IDispathStragedy
    {
        private readonly ConcurrentDictionary<Type, int> _currUsedInvoker = new ConcurrentDictionary<Type, int>();

        public ServiceRegistryMetta Select(Type interfaceType, ServiceRegistryMetta[] metta)
        {
            if (_currUsedInvoker.ContainsKey(interfaceType))
            {
                int currIdx;
                if (_currUsedInvoker.TryGetValue(interfaceType, out currIdx))
                {
                    currIdx = currIdx >= metta.Length - 1 ? 0 : currIdx + 1;
                    return metta[currIdx];
                }
            }

            _currUsedInvoker.TryAdd(interfaceType, 0);
            return metta[0];
        }
    }
}