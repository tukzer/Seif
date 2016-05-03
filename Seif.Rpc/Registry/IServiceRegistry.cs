using Seif.Rpc.Configuration;

namespace Seif.Rpc.Registry
{
    public interface IServiceRegistry
    {
        void RegisterService<T, TImpl>(ServiceRegistryMetta serviceConfiguration);

        void RegisterService(ServiceRegistryMetta serviceMetta);

        ServiceRegistryMetta[] GetServiceRegistryMetta<T>();
    }

    public class ServiceRegistryMetta
    {
        public string ServiceUrl { get; set; }
        public string InterfaceType { get; set; }
        public string InstanceType { get; set; }
        public string Protocol { get; set; }
        public string SerializeMode { get; set; }
        public bool IsEnabled { get; set; }
    }
}