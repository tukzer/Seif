namespace Seif.Rpc.Configuration
{
    public class ServiceConfiguration
    {
        public string NodeCode { get; set; }
        public string ServiceUrl { get; set; }
        public string InterfaceType { get; set; }
        public string InstanceType { get; set; }
        public string Protocol { get; set; }
        public string SerializeMode { get; set; }
        public bool IsEnabled { get; set; }
 
    }
}