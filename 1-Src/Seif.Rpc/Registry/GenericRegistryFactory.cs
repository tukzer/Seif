namespace Seif.Rpc.Registry
{
    public class GenericRegistryFactory : IRegistryFactory
    {
        public IServiceRegistry GetRegistry(RegistryOptions options)
        {
            return CreateRegistry(options);
        }

        public virtual IServiceRegistry CreateRegistry(RegistryOptions options)
        {
            return new GenericRegistry(options.RegistryDataStore, options.RegistryNotify, options.Watcher);
        }
    }
}