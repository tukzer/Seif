namespace Seif.Rpc.Registry
{
    internal class RegistryUtils
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
                ServerAddress = data.ServerAddress
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
                ServerAddress = metta.ServerAddress
            };
        }
    }
}