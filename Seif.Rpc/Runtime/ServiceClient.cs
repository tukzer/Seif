using System;
using System.Collections.Generic;
using Seif.Rpc.Common;
using Seif.Rpc.Dispatch;
using Seif.Rpc.Invoke;
using Seif.Rpc.Proxy;

namespace Seif.Rpc.Runtime
{
    public class ServiceClient : IServiceClient
    {
        private readonly List<IInvokeFilter> _filters = new List<IInvokeFilter>();

        public ServiceClient(string url, IChannel protocol)
        {
            Url = url;
            Protocol = protocol;
        }

        public ServiceClient(IChannel protocol)
        {
            Protocol = protocol;
        }

        public string Url { get; set; }

        public IChannel Protocol { get; set; }

        public IDispatcher Dispatcher { get; set; }

        public IProxyFactory ProxyFactory { get; set; }

        public List<IInvokeFilter> Filters
        {
            get { return _filters; }
        }

        public T CreateProxy<T>(ProxyOptions options)
        {
            Asserts.NotNull(Protocol,"Protocol cannot be null");
            Asserts.NotNull(ProxyFactory, "ProxyFactory cannot be null");

            if (options == null) options = new ProxyOptions();


            if (options.ServiceMetta == null)
                options.ServiceMetta = Dispatcher.Select<T>();

            var invoker = Protocol.GetInvoker();


            return ProxyFactory.CreateProxy<T>(invoker, options);
        }
    }
}