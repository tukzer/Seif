using System;
using System.Linq;
using Seif.Rpc.Common;
using Seif.Rpc.Dispatch;
using Seif.Rpc.Invoke;
using Seif.Rpc.Registry;

namespace Seif.Rpc.Default
{
    public class DefaultInvokeDispatcher :  IDispatcher
    {
        public ServiceMetta Select<T>()
        {
            var registry = SeifApplication.Current.Resolve<IServiceRegistry>();
            Asserts.NotNull(registry, "Registry cannot be null");

            var services = registry.GetServiceRegistryMetta<T>();
            if (!services.Any()) return null;

            var idx = (new Random()).Next(0, services.Length - 1);
            return services[idx];
        }
    }
}