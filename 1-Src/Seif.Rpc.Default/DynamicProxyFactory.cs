using Castle.DynamicProxy;
using Seif.Rpc.Dispatch;

namespace Seif.Rpc.Invoke.Default
{
    public class DynamicProxyFactory : IProxyFactory
    {
        public T CreateProxy<T>(ProxyOptions options, IInvokeFilter[] filters) where T: class
        {
            var dispatcher = SeifApplication.AppEnv.GlobalConfiguration.InvokeDispatcher;

            var dispatchOptions = new DispatchOptions
            {
                ServiceKind = options.ServiceKind,
                Stragedy = options.DispathStragedy
            };

            var invoker = dispatcher.Dispatch<T>(dispatchOptions);

            ProxyGenerator generator = new ProxyGenerator();
            return generator.CreateInterfaceProxyWithoutTarget<T>(new CallingInterceptor<T>(invoker, filters));
        }
    }
}