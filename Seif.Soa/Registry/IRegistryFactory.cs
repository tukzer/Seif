using Seif.Soa.Configuration;

namespace Seif.Soa.Registry
{
    public interface IRegistryFactory
    {
        IRegistry CreateRegistry(RegistryConfiguaration configuaration);
    }
}