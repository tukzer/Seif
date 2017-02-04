using System;
using System.Linq;
using Seif.Rpc.Dispatch;

namespace Seif.Rpc.Proxy
{
    public class RandomDispatcher : IDispatcher
    {
        public ServiceMetta Select<T>(ServiceMetta[] serviceMettas)
        {
            if (serviceMettas.Length <= 1) return serviceMettas.First();

            return serviceMettas[(new Random()).Next(0, serviceMettas.Length - 1)];
        }

        public ServiceMetta Select<T>()
        {
            throw new NotImplementedException();
        }
    }
}