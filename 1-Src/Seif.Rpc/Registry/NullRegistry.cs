namespace Seif.Rpc.Registry
{
    public class NullRegistry : IServiceRegistry
    {
        public void RegisterService(ServiceRegistryMetta nodeData)
        {
            throw new System.NotImplementedException();
        }

        public void UnregisterService(ServiceRegistryMetta nodeData)
        {
            throw new System.NotImplementedException();
        }

        public ServiceRegistryMetta[] GetServiceRegistryMetta<T>()
        {
            throw new System.NotImplementedException();
        }
    }
}