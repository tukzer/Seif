using Castle.DynamicProxy;
using Seif.Rpc.Dispatch;
using Seif.Rpc.Invoke;
using Seif.Rpc.Proxy;

namespace Seif.Rpc.Default
{
    public class DynamicProxyFactory : IProxyFactory
    {
        public T CreateProxy<T>(IInvoker invoker, ProxyOptions options) where T : class
        {
            // consider dispath
            //if (options.Dispatcher != null)
            //{
            //    var serviceMettas = options.ServiceRegistry.GetServiceRegistryMetta<T>();
            //    if (serviceMettas != null)
            //    {
            //        invoker.Url = options.Dispatcher.Select<T>();
            //    }
            //}

            ProxyGenerator generator = new ProxyGenerator();
            return generator.CreateInterfaceProxyWithoutTarget<T>(new CallingInterceptor<T>(invoker, options.InvokeFilters));
        }
    }
}