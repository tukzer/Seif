
namespace Seif.Rpc.Registry
{
    public interface IServiceRegistry
    {
        void RegisterService(ServiceRegistryMetta nodeData);

        void UnregisterService(ServiceRegistryMetta nodeData);

        ServiceRegistryMetta[] GetServiceRegistryMetta<T>();
    }
}