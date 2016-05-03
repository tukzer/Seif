using System;

namespace Seif.Rpc.Transport
{
    public interface IEndpoint
    {
        Uri Url { get; }

        IChannelHandler ChannelHandler { get; }

        bool IsClosed { get; }

        void Send(object message);

        void Close();

    }
}
