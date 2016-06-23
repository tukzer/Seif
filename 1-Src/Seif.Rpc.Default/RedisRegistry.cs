using System;
using System.Collections.Generic;
using System.Linq;
using Seif.Rpc.Registry;
using Seif.Rpc.Utils;
using ServiceStack.Redis;
using ServiceStack.Redis.Generic;

namespace Seif.Rpc.Default
{
    public class RedisRegistryProvider : IRegistryDataStore, IRegistryNotify, IDisposable
    {
        private readonly IRedisClient _redisClient;
        private readonly IRedisTypedClient<RegistryDataInfo> _typedClient;
        private readonly IRedisList<RegistryDataInfo> _table;

        private readonly IRedisSubscription _redisSubscription;

        private bool _isDisposed;
        private readonly ISerializer _serializer = new ServiceStackJsonSerializer();

        public RedisRegistryProvider()
        {
            if (SeifApplication.AppEnv.GlobalConfiguration.RegistryConfiguration == null)
                throw new Exception("注册中心配置有误");

            string url = SeifApplication.AppEnv.GlobalConfiguration.RegistryConfiguration.Url;
            string collectionName = "RegistryData";

            var attrs = DictionaryUtils.GetFromConfig(SeifApplication.AppEnv.GlobalConfiguration.RegistryConfiguration.AddtionalFields);
            if (attrs != null && attrs.ContainsKey(AttrKeys.Registry_RedisCollectionName) )
            {
                if (!string.IsNullOrEmpty(attrs[AttrKeys.Registry_RedisCollectionName]))
                {
                    collectionName = attrs[AttrKeys.Registry_RedisCollectionName];
                }
            }

            _redisClient = new RedisClient(url);
            _typedClient = _redisClient.As<RegistryDataInfo>();
            _table = _typedClient.Lists[collectionName];

            _redisSubscription = _redisClient.CreateSubscription();
            _redisSubscription.OnMessage += (channel, msg) =>
            {
                var data = _serializer.Deserialize<ServiceRegistryMetta>(msg);
                if (this.ServiceChanged == null) return;

                this.ServiceChanged(this, new ServiceNotifyEventArgs
                {
                    Data = data
                });
            };
        }

        public event EventHandler<ServiceNotifyEventArgs> ServiceChanged;
        public void NotifyChange(ServiceRegistryMetta data)
        {
            var message = _serializer.Serialize(data);
            _redisClient.PublishMessage(data.ServiceIdentifier, message);
        }

        public void Subscribe(string serviceIdentifier)
        {
            _redisSubscription.SubscribeToChannels(serviceIdentifier);
        }

        public void Unsubscribe(string serviceIdentifier)
        {
            _redisSubscription.UnSubscribeFromChannels(serviceIdentifier);
        }

        public void Add(RegistryDataInfo data)
        {
            var exists = _table.FirstOrDefault(p => p.Identifier == data.Identifier);

            if (exists != null)
            {
                _typedClient.RemoveItemFromList(_table, exists);
            }

            _typedClient.AddItemToList(_table, data);
            _typedClient.Save();
        }

        public void Update(RegistryDataInfo data)
        {
            var exists = _table.FirstOrDefault(p => p.Identifier == data.Identifier);

            if (exists == null) return;

            _typedClient.RemoveItemFromList(_table, exists);
            _typedClient.AddItemToList(_table, data);
            _typedClient.Save();
        }

        public void Remove(string id)
        {
            var exists = _table.FirstOrDefault(p => p.Identifier == id);

            if (exists == null) return;

            _typedClient.RemoveItemFromList(_table, exists);
            _typedClient.Save();
        }

        public IList<RegistryDataInfo> GetAllByInterface(string interfaceName)
        {
            return _table.GetAll().Where(p => p.InterfaceName == interfaceName).ToList();
        }

        #region Dispose

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Dispose(bool disposing)
        {
            if (!this._isDisposed)
            {
                if (disposing)
                {
                    if (_redisSubscription != null)
                    {
                        _redisSubscription.UnSubscribeFromAllChannels();
                        _redisSubscription.Dispose();
                    }

                    if (_redisClient != null)
                    {
                        _redisClient.UnWatch();
                        _redisClient.Dispose();
                    }
                }
            }
            _isDisposed = true;
        }

        ~RedisRegistryProvider()
        {
            Dispose(false);
        }
        #endregion
    }
}