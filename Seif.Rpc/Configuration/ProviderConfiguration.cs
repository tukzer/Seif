using System.Configuration;

namespace Seif.Rpc.Configuration
{
    public class ProviderConfiguration : ConfigurationElement
    {
        [ConfigurationProperty("Domain", IsRequired = true)]
        public string ApiDomain { get; set; }
        [ConfigurationProperty("IpAddress", IsRequired = true)]
        public string ApiIpAddress { get; set; }
        [ConfigurationProperty("NodeCode", IsRequired = true)]
        public string NodeCode { get; set; }
    }
}