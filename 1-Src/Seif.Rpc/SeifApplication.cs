using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Seif.Rpc.Configuration;
using Seif.Rpc.Invoke;
using Seif.Rpc.Registry;
using Seif.Rpc.Utils;

namespace Seif.Rpc
{
    public class SeifApplication
    {
        private bool _isInitialized = false;
        public static readonly SeifApplication AppEnv = new SeifApplication();
        private ITypeBuilder _typeBuilder;
        //private IServiceRegistry _serviceRegistry;
        //private ProviderConfiguration _providerConfiguration;
        //private ConsumerConfiguration _consumerConfiguration;
        private SeifConfiguration _seifConfiguration;
        private IInvokeFilter[] _invokeFilters;
        private IDictionary<string, ISerializer> _serializers;

        private IDictionary<string, Type> _exportedTypes = new ConcurrentDictionary<string, Type>();

        private SeifApplication()
        {
        }

        public ITypeBuilder TypeBuilder
        {
            get { return _typeBuilder; }
        }

        //public ProviderConfiguration ProviderConfiguration
        //{
        //    get { return _providerConfiguration; }
        //}

        //public ConsumerConfiguration ConsumerConfiguration
        //{
        //    get { return _consumerConfiguration; }
        //}
        public SeifConfiguration GlobalConfiguration
        {
            get { return _seifConfiguration; }
        }

        //public IServiceRegistry ServiceRegistry
        //{
        //    get { return _serviceRegistry; }
        //}

        public IDictionary<string, ISerializer> Serializers
        {
            get { return _serializers ?? (_serializers = new Dictionary<string, ISerializer>()); }
        }

        public IDictionary<string, Type> ExportedTypes
        {
            get { return _exportedTypes; }
        }

        public static void Initialize(SeifConfiguration configuration)
        {
            if (AppEnv._isInitialized)
                throw new Exception("AppEnv can only be initialized once.");

            AppEnv.InitializeApplication(configuration);
        }

        private void InitializeApplication(SeifConfiguration configuration)
        {
            if (AppEnv._isInitialized)
                throw new Exception("AppEnv can only be initialized once.");

            this._isInitialized = true;
            this._seifConfiguration = configuration;
            this._typeBuilder = configuration.TypeBuilder;
            //this._serviceRegistry = configuration.Registry;
            //this._providerConfiguration = configuration.ProviderConfiguration;
            //this._consumerConfiguration = configuration.ConsumerConfiguration;
            this._serializers = configuration.Serializers;
            //this._invokeFilters = configuration

            AppEnv.InitializeInvokeFilters();
        }

        private void InitializeInvokeFilters()
        {
            _invokeFilters = new IInvokeFilter[0];
        }

        //public static RpcContext CreateContext()
        //{
        //    return new RpcContext();
        //}

        public static T Resolve<T>()
        {
            return AppEnv.TypeBuilder.ResolveType<T>();
        }
        
        public static object ResolveByName(string name)
        {
            if (AppEnv.ExportedTypes.ContainsKey(name))
            {
                var type = AppEnv.ExportedTypes[name];
                return AppEnv.TypeBuilder.ResolveType(type.AssemblyQualifiedName);
            }

            return AppEnv.TypeBuilder.ResolveType(name);
        }

        public static ISerializer GetSerializer(string serializationMode)
        {
            if (string.IsNullOrEmpty(serializationMode)) return null;
            if (AppEnv.Serializers == null) return null;

            return AppEnv.Serializers.ContainsKey(serializationMode) ? AppEnv.Serializers[serializationMode] : null;
        }

        public static void ExposeService<T, TImpl>(ServiceRegistryMetta serviceMetta) where TImpl : class 
        {

            var registry = AppEnv.GlobalConfiguration.Registry;
            registry.RegisterService(serviceMetta);

            AppEnv._typeBuilder.RegisterType<T,TImpl>();

            AnalyzeType(typeof(T));
        }

        public static void ExposeService<T, TImpl>() where TImpl : class 
        {
            var serviceMetta = new ServiceRegistryMetta
            {
                InstanceType =  typeof(TImpl).AssemblyQualifiedName,
                InterfaceType = typeof(T).AssemblyQualifiedName,
                Protocol = AppEnv.GlobalConfiguration.ProviderConfiguration.Protocol,
                SerializeMode = AppEnv.GlobalConfiguration.ProviderConfiguration.SerializeMode,
                ApiDomain = AppEnv.GlobalConfiguration.ProviderConfiguration.ApiDomain,
                ServerAddress = AppEnv.GlobalConfiguration.ProviderConfiguration.ApiIpAddress,
                IsEnabled = true,
                Attributes = DictionaryUtils.GetFromConfig(AppEnv.GlobalConfiguration.ProviderConfiguration.AddtionalFields)
            };

            ExposeService<T, TImpl>(serviceMetta);
        }

        public static void ReferenceService<T>(ProxyOptions options, IInvokeFilter[] filters) where T : class
        {
            // 注册调用方信息
            options.ServiceKind = ServiceKind.Remote;
            options.EndpointUri = AppEnv.GlobalConfiguration.ConsumerConfiguration.Url;
            options.Attributes.Add(AttrKeys.NodeCodeConsumer, AppEnv.GlobalConfiguration.ConsumerConfiguration.NodeCode);

            IProxyFactory proxyFactory = AppEnv.GlobalConfiguration.ProxyFactory;
            var proxy = proxyFactory.CreateProxy<T>(options, AppEnv._invokeFilters.Concat(filters).ToArray());
            AppEnv._typeBuilder.RegisterType<T>(proxy);

        }

        #region Provider辅助方法

        private static void AnalyzeType(Type interfaceType)
        {
            AddToTypeCache(interfaceType);

            foreach (var method in interfaceType.GetMethods(BindingFlags.Instance | BindingFlags.Public) )
            {
                AddToTypeCache(method.ReturnType);
                foreach (var parameter in method.GetParameters())
                {
                    AddToTypeCache(parameter.ParameterType);
                }
            }
        }

        private static void AddToTypeCache(Type type)
        {
            if (type.IsPrimitive) return;
            if (type == typeof (void)) return;
            if (type == typeof (string)) return;

            if (!AppEnv.ExportedTypes.ContainsKey(type.FullName))
            {
                AppEnv.ExportedTypes.Add(type.FullName, type);
            }
        }


        #endregion

    }
}