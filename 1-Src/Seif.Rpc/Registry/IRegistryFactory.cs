namespace Seif.Rpc.Registry
{
    public interface IRegistryFactory
    {
        IServiceRegistry GetRegistry(RegistryOptions options);
    }
}