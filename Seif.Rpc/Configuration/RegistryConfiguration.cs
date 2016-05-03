using System.Configuration;

namespace Seif.Rpc.Configuration
{
    public class RegistryConfiguration : ConfigurationElement
    {
        [ConfigurationProperty("Url", IsRequired = true)]
        public string Url { get; set; } 
    }
}