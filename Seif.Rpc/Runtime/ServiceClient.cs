using System.Collections.Generic;
using Seif.Rpc.Common;
using Seif.Rpc.Dispatch;
using Seif.Rpc.Invoke;
using Seif.Rpc.Proxy;
using Seif.Rpc.Registry;

namespace Seif.Rpc.Runtime
{
    public class ServiceClient
    {
        private readonly IProxyFactory _proxyFactory;
        private readonly IChannel _protocol;
        private readonly IServiceRegistry _registry;
        private readonly IDispatcher _dispatcher;
        private readonly List<IInvokeFilter> _filters = new List<IInvokeFilter>();

        public ServiceClient(IProxyFactory proxyFactory, IChannel protocol)
        {
            _proxyFactory = proxyFactory;
            _protocol = protocol;

            _registry = SeifApplication.Current.Resolve<IServiceRegistry>();
            _dispatcher = SeifApplication.Current.Resolve<IDispatcher>();
        }

        public ServiceClient(string protocol)
        {
            Asserts.IsTrue(SeifApplication.Current.Protocols.ContainsKey(protocol), "Protocol not supported");
        
            _proxyFactory = SeifApplication.Current.Resolve<IProxyFactory>();
            _protocol = SeifApplication.Current.Protocols[protocol];

            _registry = SeifApplication.Current.Resolve<IServiceRegistry>();
            _dispatcher = SeifApplication.Current.Resolve<IDispatcher>();
        }

        public ServiceClient(IProxyFactory proxyFactory, IChannel protocol, IServiceRegistry registry,
            IDispatcher dispatcher)
        {
            _proxyFactory = proxyFactory;
            _protocol = protocol;
            _registry = registry;
            _dispatcher = dispatcher;
        }

        public IChannel Protocol
        {
            get { return _protocol; }
        }

        public IDispatcher Dispatcher
        {
            get { return _dispatcher; }
        }

        public IProxyFactory ProxyFactory
        {
            get { return _proxyFactory; }
        }

        public List<IInvokeFilter> Filters
        {
            get { return _filters; }
        }

        public T CreateProxy<T>(ProxyOptions options) where T : class 
        {
            Asserts.NotNull(Protocol, "Protocol cannot be null");
            Asserts.NotNull(ProxyFactory, "ProxyFactory cannot be null");

            if (options == null) options = new ProxyOptions();

            if (options.ServiceRegistry == null && _registry != null)
                options.ServiceRegistry = _registry;
            if (options.Dispatcher == null && _dispatcher != null)
                options.Dispatcher = _dispatcher;

            var invoker = Protocol.GetInvoker();
            return ProxyFactory.CreateProxy<T>(invoker, options);
        }
    }
}