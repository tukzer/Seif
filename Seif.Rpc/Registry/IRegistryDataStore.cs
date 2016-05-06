using System.Collections.Generic;

namespace Seif.Rpc.Registry
{
    public interface IRegistryDataStore
    {
        void Add(RegistryDataInfo data);

        void Update(string id, bool isEnabled);

        void Remove(string id);

        IList<RegistryDataInfo> GetAllByInterface(string interfaceName);
    }
}