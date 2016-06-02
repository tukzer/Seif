using System;

namespace Seif.Rpc.Registry
{
    public interface IRegistryNotify
    {
        event EventHandler<ServiceNotifyEventArgs> ServiceChanged;
        //event EventHandler<ServerNotifyEventArgs> ServerChanged;

        void NotifyChange(ServiceRegistryMetta data);

        void Subscribe(string serviceIdentifier);
    }

    public class ServiceNotifyEventArgs : EventArgs
    {
        public ServiceRegistryMetta Data { get; set; }
    }

    public class ServerNotifyEventArgs : EventArgs
    {
        public ServiceRegistryMetta[] Data { get; set; }
 
    }
}