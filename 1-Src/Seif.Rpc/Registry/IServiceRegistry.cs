
namespace Seif.Rpc.Registry
{
    public interface IServiceRegistry
    {
        void RegisterService(ServiceRegistryMetta serviceMetta);

        ServiceRegistryMetta[] GetServiceRegistryMetta<T>();
    }
}