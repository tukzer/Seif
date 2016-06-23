using System;
using Seif.Rpc.Invoke;
using Seif.Rpc.Registry;

namespace Seif.Rpc.Default
{
    public class DefaultInvokerFactory : IInvokerFactory
    {
        public IInvoker CreateInvoker(ServiceRegistryMetta options)
        {
            //var exists = SeifApplication.AppEnv.GlobalConfiguration.Invokers.ContainsKey(options.Protocol);

            //if (!exists)
            //    throw new Exception(string.Format("Cann't find invoker by name {0}", options.Protocol));

            //return SeifApplication.AppEnv.GlobalConfiguration.Invokers[options.Protocol];

            var serializer = SeifApplication.GetSerializer(options.SerializeMode);

            switch (options.Protocol.ToUpperInvariant())
            {
                case "HTTPINVOKER":
                    return new HttpInvoker(options.ServerAddress, serializer, options.Attributes);
                default:
                    return null;
            }
        }
    }
}