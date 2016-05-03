using System;
using System.Linq;
using Seif.Rpc.Configuration;
using Seif.Rpc.Invoke;
using Seif.Rpc.Invoke.Default;
using Seif.Rpc.Registry;

namespace Seif.Rpc
{
    public class SeifApplication
    {
        private bool _isInitialized = false;
        public static readonly SeifApplication AppEnv = new SeifApplication();
        private ITypeBuilder _typeBuilder;
        private IServiceRegistry _serviceRegistry;
        private ProviderConfiguration _providerConfiguration;
        private ConsumerConfiguration _consumerConfiguration;
        private IInvokeFilter[] _invokeFilters;

        private SeifApplication()
        {
        }

        public ITypeBuilder TypeBuilder
        {
            get { return _typeBuilder; }
        }

        public ProviderConfiguration ProviderConfiguration
        {
            get { return _providerConfiguration; }
        }

        public ConsumerConfiguration ConsumerConfiguration
        {
            get { return _consumerConfiguration; }
        }

        public IServiceRegistry ServiceRegistry
        {
            get { return _serviceRegistry; }
        }

        public static void Initialize(IServiceRegistry toRegistry, ProviderConfiguration providerConfiguration,ConsumerConfiguration consumerConfiguration, ITypeBuilder typeBuilder)
        {
            if(AppEnv._isInitialized)
                throw new Exception("AppEnv can only be initialized once.");

            AppEnv._isInitialized = true;
            AppEnv._typeBuilder = typeBuilder;
            AppEnv._serviceRegistry = toRegistry;
            AppEnv._providerConfiguration = providerConfiguration;
            AppEnv._consumerConfiguration = consumerConfiguration;

            AppEnv.InitializeInvokeFilters();
        }

        private void InitializeInvokeFilters()
        {
            _invokeFilters = new IInvokeFilter[0];
        }


        public static RpcContext CreateContext()
        {
            return new RpcContext();
        }

        public static T Resolve<T>()
        {
            return AppEnv._typeBuilder.ResolveType<T>();
        }

        public static void ExposeService<T, TImpl>(ServiceConfiguration serviceConfiguration)
        {
            var serviceMetta = new ServiceRegistryMetta
            {
                InstanceType = serviceConfiguration.InstanceType,
                InterfaceType = serviceConfiguration.InterfaceType,
                Protocol = serviceConfiguration.Protocol,
                SerializeMode = serviceConfiguration.SerializeMode,
                ServiceUrl = serviceConfiguration.ServiceUrl,
                IsEnabled = true
            };

            AppEnv._serviceRegistry.RegisterService<T, TImpl>(serviceMetta);
        }

        public static void ReferenceService<T>(ProxyOptions options, IInvokeFilter[] filters) where T : class
        {
            // 注册调用方信息
            options.ServiceKind = ServiceKind.Remote;
            RegisterService<T>(options, filters);
        }

        private static void RegisterService<T>(ProxyOptions options, IInvokeFilter[] filters) where T : class
        {
            options.EndpointUri = AppEnv._consumerConfiguration.Url;
            options.Attributes.Add(AttrKeys.NodeCodeConsumer, AppEnv._consumerConfiguration.NodeCode);

            IProxyFactory proxyFactory = new DynamicProxyFactory();
            var proxy = proxyFactory.CreateProxy<T>(options, AppEnv._invokeFilters.Concat(filters).ToArray());
            AppEnv._typeBuilder.RegisterType<T>(proxy);
        }

        public static void RegisterService<T>() where T : class
        {
            ProxyOptions options = new ProxyOptions();
            options.ServiceKind = ServiceKind.Remote;
            RegisterService<T>(options, new IInvokeFilter[0]);
        }

    }
}