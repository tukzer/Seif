using Seif.Rpc.Common;
using Seif.Rpc.Invoke;

namespace Seif.Rpc.Runtime
{
    public interface IChannel
    {
        ISerializer Serializer { get; set; }
        IInvokerFactory InvokerFactory { get; set; }

        IInvoker GetInvoker();
    }
}