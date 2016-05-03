using System;
using System.Collections.Concurrent;
using Seif.Core;
using Seif.Soa.Configuration;
using Seif.Soa.Registry;

namespace Seif.Soa.Default
{
    public class DefaultClient : IRpcClient
    {
        private ConcurrentDictionary<string, IRegistry> _registries;

        public DefaultClient()
        {
            _registries = new ConcurrentDictionary<string, IRegistry>();
        }

        public Uri Url
        {
            get { throw new NotImplementedException(); }
        }

        public void Close()
        {
            throw new NotImplementedException();
        }

        public void Init(ConsumerConfiguration configuaration)
        {
            foreach (var registryUrl in configuaration.RegistryList)
            {
                // Get Service Registry
                var registryFactory = ApplicationContext.Get<IRegistryFactory>();
                var registryConfig = new RegistryConfiguaration
                {
                    Url = registryUrl
                };

                var registry = registryFactory.CreateRegistry(registryConfig);
                _registries.TryAdd(registry.Url, registry);
            }
        }

        public void Refer<T>() where T : class
        {
            // Step 1. Get Service Registry
            var registry = ApplicationContext.Get<IRegistry>();
            var invoker = registry.GetInvokers<T>();

            var proxyFacotry = new DynamicProxyFactory(invoker);

            var instance = proxyFacotry.CreateProxy<T>(Url.ToString());
            ApplicationContext.SetDefault(instance);
        }

        public Core.Logging.ILogFactory LogFactory
        {
            get { throw new NotImplementedException(); }
        }

        public Client.Filters.IInvokeFilter[] InvokeFilters
        {
            get { throw new NotImplementedException(); }
        }
    }
}