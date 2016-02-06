using System;

namespace Seif.Rpc.Transport
{
    public interface IChannelHandler
    {
        void OnConnected(IChannel channel);

        void OnDisconnected(IChannel channel);

        void OnSent(IChannel channel, object message);

        void OnReceived(IChannel channel, object message);

        void OnException(IChannel channel, Exception ex);

    }
}