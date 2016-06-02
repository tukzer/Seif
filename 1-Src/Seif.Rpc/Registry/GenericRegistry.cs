using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Seif.Rpc.Registry
{
    public class GenericRegistry : IServiceRegistry
    {
        private readonly IRegistryDataStore _registryDataStore;
        private readonly IRegistryNotify _registryNotify;

        private readonly ConcurrentDictionary<string, ConcurrentDictionary<string, ServiceRegistryMetta>>
            _availableServer = new ConcurrentDictionary<string, ConcurrentDictionary<string, ServiceRegistryMetta>>();

        public GenericRegistry(IRegistryDataStore registryDataStore,IRegistryNotify registryNotify)
        {
            _registryDataStore = registryDataStore;
            _registryNotify = registryNotify;

            if (_registryNotify != null)
                _registryNotify.ServiceChanged += RegistryNotifyServerChanged;
        }

        void RegistryNotifyServerChanged(object sender, ServiceNotifyEventArgs e)
        {
            var interfaceName = e.Data.InterfaceType;
            if (_availableServer.ContainsKey(interfaceName))
            {
                ServiceRegistryMetta metta;

                var bag = _availableServer[interfaceName];
                if (bag.TryGetValue(e.Data.ServiceIdentifier, out metta))
                {
                    if (!e.Data.IsEnabled)
                    {
                        bag.TryRemove(e.Data.ServiceIdentifier, out metta);
                        return;
                    }

                    bag.TryUpdate(e.Data.ServiceIdentifier, e.Data, metta);
                }
                else
                {
                    bag.TryAdd(e.Data.ServiceIdentifier, e.Data);
                }
            }
            else
            {
                var concurrent = new ConcurrentDictionary<string, ServiceRegistryMetta>();
                concurrent.TryAdd(interfaceName, e.Data);
                _availableServer.TryAdd(interfaceName, concurrent);
            }
        }

        public void RegisterService(ServiceRegistryMetta serviceMetta)
        {
            var data = RegistryUtils.ToDataObject(serviceMetta);
            if (data == null)
                throw new SeifException("Cannot register service, no data is provided.");

            _registryDataStore.Add(data);
            _registryNotify.NotifyChange(serviceMetta);
        }

        public ServiceRegistryMetta[] GetServiceRegistryMetta<T>()
        {
            var interfaceName = typeof (T).FullName;
            var list = _registryDataStore.GetAllByInterface(interfaceName);


            if (_availableServer.ContainsKey(interfaceName))
            {
                return _availableServer[interfaceName].Values.ToArray();
            }

            var bag = new ConcurrentDictionary<string, ServiceRegistryMetta>(list.Select(p =>
            {
                var metta = RegistryUtils.ToPlainObject(p);
                return new KeyValuePair<string, ServiceRegistryMetta>(metta.ServiceIdentifier, metta);
            }));
            _availableServer.AddOrUpdate(interfaceName, bag, (s, mettas) => bag);
            return _availableServer[interfaceName].Values.ToArray();
        }
    }
}