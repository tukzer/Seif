using System.Collections.Generic;

namespace Seif.Rpc.Registry
{
    public class ServiceRegistryMetta
    {
        public string ApiDomain { get; set; }
        public string ServerAddress { get; set; }
        public string InterfaceType { get; set; }
        public string InstanceType { get; set; }
        public string Protocol { get; set; }
        public string SerializeMode { get; set; }
        public bool IsEnabled { get; set; }
        public IDictionary<string,string> Attributes { get; set; }

        public string ServiceIdentifier
        {
            get { return string.Format("{0}-{1}", ServerAddress, InterfaceType); }
        }
    }

}