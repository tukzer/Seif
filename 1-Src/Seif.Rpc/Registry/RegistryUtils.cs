using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using Seif.Rpc.Utils;

namespace Seif.Rpc.Registry
{
    public class RegistryUtils
    {
        public static ServiceRegistryMetta ToPlainObject(RegistryDataInfo data)
        {
            if (data.ApiDomain == null) return null;

            return new ServiceRegistryMetta
            {
                ApiDomain = data.ApiDomain,
                InterfaceType = data.InterfaceName,
                InstanceType = data.InstanceName,
                Protocol = data.Protocol,
                SerializeMode = data.SerializeMode,
                IsEnabled = data.IsEnabled,
                ServerAddress = data.ServerAddress,
                Attributes = DictionaryUtils.GetFromUrl(data.AdditionalFields)
            };
        }

        public static RegistryDataInfo ToDataObject(ServiceRegistryMetta metta)
        {
            if (metta.ApiDomain == null) return null;

            return new RegistryDataInfo
            {
                ApiDomain = metta.ApiDomain,
                InterfaceName = metta.InterfaceType,
                InstanceName = metta.InstanceType,
                Protocol = metta.Protocol,
                SerializeMode = metta.SerializeMode,
                IsEnabled = metta.IsEnabled,
                ServerAddress = metta.ServerAddress,
                AdditionalFields = DictionaryUtils.ToUrlString( metta.Attributes)
            };
        }
    }
}