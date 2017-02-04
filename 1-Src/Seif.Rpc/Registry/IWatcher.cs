using System;
using Seif.Rpc.Invoke;

namespace Seif.Rpc.Registry
{
    public interface IWatcher
    {
        event Action<string> MarkDead;

        void Run();

        void Watch(string serverAddress, WatcherOptions options);

        void Unwatch(string serverAddress);

        void MarkAsAlive(string serverAddress);

        bool IsInWatchList(string serverAddress);
    }

    public class WatcherOptions
    {
        public ServiceRegistryMetta ServiceMetta { get; set; }
        public IInvoker Invoker { get; set; }
    }

}