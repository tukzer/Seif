using System.Collections.Generic;
using System.Configuration;
using Seif.Rpc.Dispatch;
using Seif.Rpc.Invoke;
using Seif.Rpc.Registry;
using Seif.Rpc.Utils;

namespace Seif.Rpc.Configuration
{
    public class SeifConfiguration : ConfigurationSection
    {
        private IProxyFactory _proxyFactory;
        private ITypeBuilder _typeBuilder;
        private IDictionary<string, ISerializer> _serializers;
        //private IDictionary<string, IInvoker> _invokers;
        private IDictionary<string, IInvokeFilter> _invokeFilters;
        private IInvokerFactory _invokerFactory;
        private IInvokeDispatcher _invokeDispatcher;
        private IServiceRegistry _registry;

        [ConfigurationProperty("ProxyFactory", IsRequired = false)]
        public string ProxyFactoryDefinition
        {
            get { return this["ProxyFactory"].ToString(); }
            set { this["ProxyFactory"] = value; }
        }

        [ConfigurationProperty("TypeBuilder", IsRequired = false)]
        public string TypeBuilderDefinition
        {
            get { return this["TypeBuilder"].ToString(); }
            set { this["TypeBuilder"] = value; }
        }

        [ConfigurationProperty("Serializers", IsRequired = false)]
        public KeyValueConfigurationCollection SerializerDefinition
        {
            get { return this["Serializers"] as KeyValueConfigurationCollection; }
            set { this["Serializers"] = value; }
        }

        //[ConfigurationProperty("Invokers", IsRequired = false,
        //    Options = ConfigurationPropertyOptions.IsDefaultCollection)]
        //public KeyValueConfigurationCollection InvokersDefinition
        //{
        //    get { return this["Invokers"] as KeyValueConfigurationCollection; }
        //    set { this["Invokers"] = value; }
        //}

        [ConfigurationProperty("InvokeFilters", IsRequired = false)]
        public KeyValueConfigurationCollection InvokeFilterDefinition
        {
            get { return this["InvokeFilters"] as KeyValueConfigurationCollection; }
            set { this["InvokeFilters"] = value; }
        }

        [ConfigurationProperty("InvokerFactory", IsRequired = false)]
        public string InvokerFactoryDefinition
        {
            get { return this["InvokerFactory"].ToString(); }
            set { this["InvokerFactory"] = value; }
        }

        [ConfigurationProperty("InvokerDispatcher", IsRequired = false)]
        public string InvokerDispatcherDefinition
        {
            get { return this["InvokerDispatcher"].ToString(); }
            set { this["InvokerDispatcher"] = value; }
        }

        [ConfigurationProperty("Registry", IsRequired = false)]
        public RegistryConfiguration RegistryConfiguration
        {
            get { return this["Registry"] as RegistryConfiguration; }
            set { this["Registry"] = value; }
        }

        [ConfigurationProperty("Provider", IsRequired = false)]
        public ProviderConfiguration ProviderConfiguration
        {
            get { return this["Provider"] as ProviderConfiguration; }
            set { this["Provider"] = value; }
        }

        [ConfigurationProperty("Consumer", IsRequired = false)]
        public ConsumerConfiguration ConsumerConfiguration
        {
            get { return this["Consumer"] as ConsumerConfiguration; }
            set { this["Consumer"] = value; }
        }

        public IProxyFactory ProxyFactory
        {
            get
            {
                if (_proxyFactory == null)
                {
                    _proxyFactory = TypeUtils.LoadInstance<IProxyFactory>(this.ProxyFactoryDefinition);
                }
                return _proxyFactory;
            }
        }        
        public IServiceRegistry Registry
        {
            get
            {
                if (_registry == null)
                {
                    var registryOptions = new RegistryOptions
                    {
                        RegistryDataStore = RegistryConfiguration.RegistryDataStore,
                        RegistryNotify = RegistryConfiguration.RegistryNotify,
                        Url = RegistryConfiguration.Url
                    };
                    _registry = RegistryConfiguration.RegistryFactory.GetRegistry(registryOptions);
                }
                return _registry;
            }
        }
        public ITypeBuilder TypeBuilder
        {
            get
            {
                if (_typeBuilder == null)
                {
                    _typeBuilder = TypeUtils.LoadInstance<ITypeBuilder>(this.TypeBuilderDefinition);
                }

                return _typeBuilder;
            }
        }
        public IDictionary<string, ISerializer> Serializers
        {
            get
            {
                if (_serializers == null)
                {
                    _serializers = new Dictionary<string, ISerializer>();

                    foreach (KeyValueConfigurationElement def in SerializerDefinition)
                    {
                        var serializer = TypeUtils.LoadInstance<ISerializer>(def.Value);
                        if (serializer == null) continue;

                        _serializers.Add(def.Key, serializer);
                    }
                }

                return _serializers;
            }
        }
        //public IDictionary<string, IInvoker> Invokers
        //{
        //    get
        //    {
        //        if (_invokers == null)
        //        {
        //            _invokers = new Dictionary<string, IInvoker>();

        //            foreach (KeyValueConfigurationElement def in InvokersDefinition)
        //            {
        //                var invoker = TypeUtils.LoadInstance<IInvoker>(def.Value);
        //                if (invoker == null) continue;

        //                _invokers.Add(def.Key, invoker);
        //            }
        //        }

        //        return _invokers;
        //    }
        //}

        public IDictionary<string, IInvokeFilter> InvokerFilters
        {
            get
            {
                if (_invokeFilters == null)
                {
                    _invokeFilters = new Dictionary<string, IInvokeFilter>();

                    foreach (KeyValueConfigurationElement def in InvokeFilterDefinition)
                    {
                        var invokeFilter = TypeUtils.LoadInstance<IInvokeFilter>(def.Value);
                        if (invokeFilter == null) continue;

                        _invokeFilters.Add(def.Key, invokeFilter);
                    }
                }

                return _invokeFilters;
            }
        }
        public IInvokerFactory InvokerFactory
        {
            get
            {
                if (_invokerFactory == null)
                {
                    _invokerFactory = TypeUtils.LoadInstance<IInvokerFactory>(this.InvokerFactoryDefinition);
                }
                return _invokerFactory;
            }
        }
        public IInvokeDispatcher InvokeDispatcher
        {
            get
            {
                if (_invokeDispatcher == null)
                {
                    _invokeDispatcher = TypeUtils.LoadInstance<IInvokeDispatcher>(this.InvokerDispatcherDefinition);
                }

                return _invokeDispatcher;
            }
        }

    }
}