using System;
using Seif.Rpc.Registry;

namespace Seif.Rpc.Dispatch
{
    public interface IDispathStragedy
    {
        ServiceRegistryMetta Select(Type interfaceType, ServiceRegistryMetta[] metta);
    }
}