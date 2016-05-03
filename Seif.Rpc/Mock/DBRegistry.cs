using Seif.Rpc.Configuration;
using Seif.Rpc.Registry;

namespace Seif.Rpc.Mock
{
    public class DBRegistry : IServiceRegistry
    {
        public void RegisterService<T, TImpl>(ServiceRegistryMetta serviceConfiguration)
        {
            throw new System.NotImplementedException();
        }

        public void RegisterService(ServiceRegistryMetta serviceMetta)
        {
            throw new System.NotImplementedException();
        }

        public ServiceRegistryMetta[] GetServiceRegistryMetta<T>()
        {
            throw new System.NotImplementedException();
        }
    }
}