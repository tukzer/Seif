using Seif.Rpc.Registry;

namespace Seif.Rpc.Invoke.Default
{
    public class DefaultInvokerFactory : IInvokerFactory
    {
        public IInvoker CreateInvoker(ServiceRegistryMetta options)
        {
            var serializer = SeifApplication.GetSerializer(options.SerializeMode);

            switch (options.Protocol.ToUpperInvariant())
            {
                case "HTTP":
                    return new HttpInvoker(options.ApiDomain, serializer, options.Attributes);
                default:
                    return null;
            }
        }
    }
}