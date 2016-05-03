using System;

namespace Seif.Rpc.Transport
{
    public interface ITransporter
    {
        IRpcServer Bind(Uri url, IChannelHandler handler);

        IRpcClient Connect(Uri url, IChannelHandler handler);
    }
}