using System.Configuration;

namespace Seif.Rpc.Configuration
{
    public class ProviderConfiguration : ConfigurationElement
    {
        [ConfigurationProperty("Url", IsRequired = true)]
        public string Url { get; set; }
        [ConfigurationProperty("NodeCode", IsRequired = true)]
        public string NodeCode { get; set; }
    }
}