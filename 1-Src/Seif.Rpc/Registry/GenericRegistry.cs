using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Seif.Rpc.Utils;

namespace Seif.Rpc.Registry
{
    public class GenericRegistry : IServiceRegistry
    {
        private readonly IRegistryDataStore _registryDataStore;
        private readonly IRegistryNotify _registryNotify;
        private readonly IWatcher _watcher;

        private readonly ConcurrentDictionary<string, ConcurrentDictionary<string, ServiceRegistryMetta>>
            _availableServer = new ConcurrentDictionary<string, ConcurrentDictionary<string, ServiceRegistryMetta>>();

        //private readonly ConcurrentBag<string> _activeServer = new ConcurrentBag<string>();

        public GenericRegistry(IRegistryDataStore registryDataStore,IRegistryNotify registryNotify, IWatcher watcher)
        {
            _registryDataStore = registryDataStore;
            _registryNotify = registryNotify;
            _watcher = watcher;

            if (_registryNotify != null)
            {
                _registryNotify.ServiceChanged += RegistryNotifyOnServiceChanged;
            }

            if (_watcher != null)
            {
                _watcher.MarkDead += delegate(string s)
                {
                    foreach (var serviceList in _availableServer.Values)
                    {
                        foreach (var metta in serviceList.Where(p => p.Value.ServerAddress == s))
                        {
                            UnregisterService(metta.Value);
                        }
                    }
                };  
            }
        }

        private void RegistryNotifyOnServiceChanged(object sender, ServiceNotifyEventArgs e)
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
                        Unwatch(e.Data);
                        return;
                    }

                    bag.TryUpdate(e.Data.ServiceIdentifier, e.Data, metta);
                }
                else
                {
                    bag.TryAdd(e.Data.ServiceIdentifier, e.Data);
                    Watch(e.Data);
                }
            }
            else
            {
                var concurrent = new ConcurrentDictionary<string, ServiceRegistryMetta>();
                concurrent.TryAdd(interfaceName, e.Data);
                _availableServer.TryAdd(interfaceName, concurrent);
            }
        }

        public void RegisterService(ServiceRegistryMetta nodeData)
        {
            _registryDataStore.Add(nodeData.ToDataObject());
            _registryNotify.NotifyChange(nodeData);
        }

        public void UnregisterService(ServiceRegistryMetta nodeData)
        {
            _registryDataStore.Remove(nodeData.ServiceIdentifier);
            nodeData.IsEnabled = false;
            _registryNotify.NotifyChange(nodeData);
        }

        public ServiceRegistryMetta[] GetServiceRegistryMetta<T>()
        {
            var interfaceName = typeof (T).AssemblyQualifiedName;
            var list = _registryDataStore.GetAllByInterface(interfaceName);


            if (_availableServer.ContainsKey(interfaceName))
            {
                return _availableServer[interfaceName].Values.ToArray();
            }

            var bag = new ConcurrentDictionary<string, ServiceRegistryMetta>(list.Select(p =>
            {
                var metta = p.ToPlainObject();
                return new KeyValuePair<string, ServiceRegistryMetta>(metta.ServiceIdentifier, metta);
            }));
            _availableServer.AddOrUpdate(interfaceName, bag, (s, mettas) => bag);

            foreach (var item in list)
            {
                 Watch(item.ToPlainObject());
            }

            return _availableServer[interfaceName].Values.ToArray();
        }

        protected virtual void Watch(ServiceRegistryMetta metta)
        {
            if (_watcher == null) return;

            _watcher.Watch(metta.ServerAddress, new WatcherOptions
            {
                ServiceMetta = metta
            });
        }

        protected virtual void Unwatch(ServiceRegistryMetta metta)
        {
            if (_watcher == null) return;

            if(!_watcher.IsInWatchList(metta.ServerAddress))
            {
                return;
            }

            _watcher.Unwatch(metta.ServerAddress);
        }
    }
}