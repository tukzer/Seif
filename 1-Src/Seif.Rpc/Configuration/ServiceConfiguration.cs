using System.Configuration;

namespace Seif.Rpc.Configuration
{
    public class ServiceConfiguration : ConfigurationElement
    {
        [ConfigurationProperty("NodeCode", IsRequired = true)]
        public string NodeCode
        {
            get { return this["NodeCode"].ToString(); }
            set { this["NodeCode"] = value; }
        }

        [ConfigurationProperty("ApiDomain", IsRequired = true)]
        public string ApiDomain
        {
            get { return this["ApiDomain"].ToString(); }
            set { this["ApiDomain"] = value; }
        }

        [ConfigurationProperty("InterfaceType", IsRequired = true)]
        public string InterfaceType
        {
            get { return this["InterfaceType"].ToString(); }
            set { this["InterfaceType"] = value; }
        }

        [ConfigurationProperty("InstanceType", IsRequired = true)]
        public string InstanceType
        {
            get { return this["InstanceType"].ToString(); }
            set { this["InstanceType"] = value; }
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

        [ConfigurationProperty("IsEnabled", IsRequired = true)]
        public bool IsEnabled
        {
            get
            {
                bool isEnabled;

                if (bool.TryParse(this["IsEnabled"].ToString(), out isEnabled))
                {
                    return isEnabled;
                }

                return true;
            }
            set { this["IsEnabled"] = value; }

        }

        [ConfigurationProperty("AddtionalFields", IsRequired = false)]
        public KeyValueConfigurationCollection AddtionalFields
        {
            get { return this["AddtionalFields"] as KeyValueConfigurationCollection; }
            set { this["AddtionalFields"] = value; }
        }
    }
}