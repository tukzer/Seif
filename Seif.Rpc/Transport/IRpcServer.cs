using System.Collections.Generic;

namespace Seif.Rpc.Transport
{
    public interface IRpcServer : IEndpoint
    {
        bool IsBound { get; }

        IEnumerable<IChannel> Channels { get; }

        IChannel GetChannel(string url);
    }
}