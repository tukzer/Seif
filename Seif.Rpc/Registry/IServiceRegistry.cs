using Seif.Rpc.Runtime;

namespace Seif.Rpc.Registry
{
    public interface IServiceRegistry : IServiceEndpoint
    {
        void RegisterService(ServiceMetta nodeData);

        void UnregisterService(ServiceMetta nodeData);

        ServiceMetta[] GetServiceRegistryMetta<T>();

    }
}