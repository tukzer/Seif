using Seif.Rpc.Configuration;
using Seif.Rpc.Invoke;
using Seif.Rpc.Registry;

namespace Seif.Rpc.Mock
{
    public class MockServer
    {
        public static void Main()
        {
            var servConfig = new ProviderConfiguration();
            servConfig.ApiDomain = "api.aaa.com";
            var clientConfig = new ConsumerConfiguration();

            var registry = new GenericRegistry(null,null);
            SeifApplication.Initialize(registry, servConfig,clientConfig, null);

            SeifApplication.ExposeService<IMockService, MockService>(new ServiceConfiguration());
        } 
    }

    public class MockClient
    {
        public static void Main()
        {
            var clientConfig = new ConsumerConfiguration();

            var registry = new GenericRegistry(null, null);
            SeifApplication.Initialize(registry, null, clientConfig, null);
            SeifApplication.ReferenceService<IMockService>(new ProxyOptions(), new IInvokeFilter[0]);

        }
    }
}