using Seif.Rpc.Common;

namespace Seif.Rpc.Invoke
{
    public interface IInvoker
    {
        string Url { get; }
        ISerializer Serializer { get; }
        RpcResult Call(RpcInvocation invocation);
    }
}