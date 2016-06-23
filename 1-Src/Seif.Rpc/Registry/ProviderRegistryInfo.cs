using System.Collections;
using System.Collections.Generic;

namespace Seif.Rpc.Registry
{
    /// <summary>
    /// Service Endpoint metta data
    /// </summary>
    public class ProviderRegistryInfo
    {
        public ProviderRegistryInfo()
        {
            Services = new List<ServiceRegistryInfo>();
        }

        public string NodeCode { get; set; }
        public string ApiDomain { get; set; }
        public string ServerAddress { get; set; }
        public string Protocol { get; set; }
        public string SerializeMode { get; set; }
        public bool IsEnabled { get; set; }
        public string Remark { get; set; }
        public IDictionary<string,string> AdditionalFields { get; set; }

        public IList<ServiceRegistryInfo> Services { get; set; }

        public string HostIdentifier
        {
            get { return string.Format("{0}@{1}", ApiDomain, ServerAddress); }
        }

    }

    public class ServiceRegistryInfo
    {
        public string NodeCode { get; set; }
        public string InterfaceName { get; set; }
        public string InstanceName { get; set; }
        public bool IsEnabled { get; set; }

        public string ServiceIdentifier
        {
            get { return InterfaceName; }
        }
    }
}