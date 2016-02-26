using Castle.DynamicProxy;
using Seif.Soa.Client;
using Seif.Soa.Proxy;

namespace Seif.Soa.Default
{
    public class DynamicProxyFactory : IProxyFactory 
    {
        private readonly IInvoker _invoker;

        public DynamicProxyFactory(IInvoker invoker)
        {
            _invoker = invoker;
        }

        public T CreateProxy<T>(string url) where T : class
        {
            var invoker = GetInvoker<T>();
            ProxyGenerator generator = new ProxyGenerator();
            return generator.CreateInterfaceProxyWithoutTarget<T>(new CallingInterceptor<T>(invoker, url));
        }

        public IInvoker<T> GetInvoker<T>() where T : class 
        {
            return _invoker as IInvoker<T>;
        }
    }
}