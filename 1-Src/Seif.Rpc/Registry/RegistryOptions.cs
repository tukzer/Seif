namespace Seif.Rpc.Registry
{
    public class RegistryOptions
    {
        public string Url { get; set; }
        public IRegistryDataStore RegistryDataStore { get; set; }
        public IRegistryNotify RegistryNotify { get; set; }
        public IWatcher Watcher { get; set; }
    }
}