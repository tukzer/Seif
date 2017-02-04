using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;

namespace Seif.Rpc.Registry
{
    public class GenericWatcher : IWatcher
    {
        private readonly ConcurrentDictionary<string, DateTime> _activeServer = new ConcurrentDictionary<string, DateTime>();

        private readonly ConcurrentDictionary<string, WatcherOptions> _watcherOptions =
            new ConcurrentDictionary<string, WatcherOptions>();

        public event Action<string> MarkDead;

        public void Run()
        {
            var idleMaxTime = SeifApplication.AppEnv.GlobalConfiguration.RegistryConfiguration.IdleMaxTime;
            Task.Factory.StartNew(() =>
            {
                Ping(idleMaxTime);
            });
        }

        protected virtual void Ping(int idleMaxTime)
        {
            foreach (var address in _activeServer.Where(p => p.Value >= DateTime.Now.AddSeconds(idleMaxTime)))
            {
                WatcherOptions options;

                if (_watcherOptions.TryGetValue(address.Key, out options))
                {
                    if (options.ServiceMetta != null && options.Invoker == null)
                    {
                        options.Invoker =
                            SeifApplication.AppEnv.GlobalConfiguration.InvokerFactory.CreateInvoker(options.ServiceMetta);
                    }

                    if (options.Invoker != null)
                    {
                        options.Invoker.Invoke(null);
                    }
                }


            }
        }

        public void Watch(string serverAddress, WatcherOptions options)
        {
            if (IsInWatchList(serverAddress))
                return;

            if (_activeServer.TryAdd(serverAddress, DateTime.Now))
            {

                _watcherOptions.TryAdd(serverAddress, options);
            }
        }

        public void Unwatch(string serverAddress)
        {
            if (!IsInWatchList(serverAddress))
                return;

            DateTime time;
            _activeServer.TryRemove(serverAddress, out time);
            WatcherOptions options;
            _watcherOptions.TryRemove(serverAddress, out options);
        }

        public void MarkAsAlive(string serverAddress)
        {
            if (!IsInWatchList(serverAddress))
                return;

            _activeServer.TryUpdate(serverAddress, DateTime.Now, _activeServer[serverAddress]);
        }

        public bool IsAlive(string serverAddress)
        {
            if (!IsInWatchList(serverAddress))
                return false;

            return _activeServer.ContainsKey(serverAddress);
        }

        public bool IsInWatchList(string serverAddress)
        {
            return _activeServer.ContainsKey(serverAddress);
        }
    }
}