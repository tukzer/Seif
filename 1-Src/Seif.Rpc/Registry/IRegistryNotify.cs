using System;

namespace Seif.Rpc.Registry
{
    public interface IRegistryNotify
    {
        event EventHandler<ServiceNotifyEventArgs> ServiceChanged;

        void NotifyChange(ServiceRegistryMetta data);

        void Subscribe(string serviceIdentifier);

        void Unsubscribe(string serviceIdentifier);
    }

    public class ServiceNotifyEventArgs : EventArgs
    {
        public ServiceRegistryMetta Data { get; set; }
    }

}