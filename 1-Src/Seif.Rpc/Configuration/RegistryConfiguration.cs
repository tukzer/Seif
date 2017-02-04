using System.Configuration;
using Seif.Rpc.Registry;
using Seif.Rpc.Utils;

namespace Seif.Rpc.Configuration
{
    public class RegistryConfiguration : ConfigurationElement
    {
        private IRegistryDataStore _dataStore;
        private IRegistryNotify _notify;
        private IRegistryFactory _factory;

        [ConfigurationProperty("Url", IsRequired = true)]
        public string Url
        {
            get { return this["Url"].ToString(); }
            set { this["Url"] = value; }
        }
        [ConfigurationProperty("IdleMaxTime", IsRequired = false)]
        public int IdleMaxTime
        {
            get
            {
                var idleTime = this["IdleMaxTime"].ToString();
                int idleMaxTime;
                if (int.TryParse(idleTime, out idleMaxTime))
                {
                    return idleMaxTime;
                }

                return 0;
            }
            set { this["IdleMaxTime"] = value; }
        }

        [ConfigurationProperty("Store", IsRequired = true)]
        public string StoreDefinition
        {
            get { return this["Store"].ToString(); }
            set { this["Store"] = value; }
        }

        [ConfigurationProperty("Notify", IsRequired = true)]
        public string NotifyDefinition
        {
            get { return this["Notify"].ToString(); }
            set { this["Notify"] = value; }
        }

        [ConfigurationProperty("RegistryFactory", IsRequired = true)]
        public string RegistryFactoryDefinition
        {
            get { return this["RegistryFactory"].ToString(); }
            set { this["RegistryFactory"] = value; }
        }
        [ConfigurationProperty("AddtionalFields", IsRequired = false)]
        public KeyValueConfigurationCollection AddtionalFields
        {
            get { return this["AddtionalFields"] as KeyValueConfigurationCollection; }
            set { this["AddtionalFields"] = value; }
        }


        public IRegistryDataStore RegistryDataStore
        {
            get {
                return _dataStore ?? (_dataStore = TypeUtils.LoadInstance<IRegistryDataStore>(this.StoreDefinition));
            }
        }

        public IRegistryNotify RegistryNotify
        {
            get { return _notify ?? (_notify = TypeUtils.LoadInstance<IRegistryNotify>(this.StoreDefinition)); }
        }

        public IRegistryFactory RegistryFactory
        {
            get {
                return _factory ?? (_factory = TypeUtils.LoadInstance<IRegistryFactory>(this.RegistryFactoryDefinition));
            }
        }

    }
}