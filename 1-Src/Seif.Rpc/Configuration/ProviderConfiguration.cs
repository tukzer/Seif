using System.Configuration;

namespace Seif.Rpc.Configuration
{
    public class ProviderConfiguration : ConfigurationElement
    {
        public ProviderConfiguration()
        {
            AddtionalFields = new KeyValueConfigurationCollection();
        }

        [ConfigurationProperty("Domain", IsRequired = true)]
        public string ApiDomain
        {
            get { return this["Domain"].ToString(); }
            set { this["Domain"] = value; }
        }

        [ConfigurationProperty("IpAddress", IsRequired = true)]
        public string ApiIpAddress
        {
            get { return this["IpAddress"].ToString(); }
            set { this["IpAddress"] = value; }
        }

        [ConfigurationProperty("NodeCode", IsRequired = true)]
        public string NodeCode
        {
            get { return this["NodeCode"].ToString(); }
            set { this["NodeCode"] = value; }
        }

        [ConfigurationProperty("Protocol", IsRequired = true)]
        public string Protocol
        {
            get { return this["Protocol"].ToString(); }
            set { this["Protocol"] = value; }
        }

        [ConfigurationProperty("SerializeMode", IsRequired = true)]
        public string SerializeMode
        {
            get { return this["SerializeMode"].ToString(); }
            set { this["SerializeMode"] = value; }
        }

        [ConfigurationProperty("AddtionalFields", IsRequired = false)]
        public KeyValueConfigurationCollection AddtionalFields
        {
            get { return this["AddtionalFields"] as KeyValueConfigurationCollection; }
            set { this["AddtionalFields"] = value; }
        }


        public ISerializer GetSerializer()
        {
            if (SeifApplication.AppEnv.GlobalConfiguration == null) return null;
            if (SeifApplication.AppEnv.GlobalConfiguration.Serializers == null) return null;

            if (SeifApplication.AppEnv.GlobalConfiguration.Serializers.ContainsKey(SerializeMode))
                return SeifApplication.AppEnv.GlobalConfiguration.Serializers[this.SerializeMode];

            return null;
        }
    }
}