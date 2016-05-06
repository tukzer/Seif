using System.IO;
using Seif.Rpc.Registry;

namespace Seif.Rpc.Invoke.Default
{
    public class DefaultInvokerFactory : IInvokerFactory
    {
        public IInvoker CreateInvoker(ServiceRegistryMetta options)
        {
            switch (options.Protocol)
            {
                case "HTTP":
                    return new HttpInvoker(options.ApiDomain, null);
                default:
                    return null;
            }
        }
    }
}