namespace Seif.Rpc.Properties
{
    public interface IInvoker<T>
    {
        T ServiceInterface { get; }

        RpcResult<T> Invoke(IInvocation invocation);
    }
}