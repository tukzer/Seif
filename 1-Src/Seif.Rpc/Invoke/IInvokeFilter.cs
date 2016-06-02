namespace Seif.Rpc.Invoke
{
    public interface IInvokeFilter
    {
        IInvocation PreInvoke(IInvocation invocation);

        InvokeResult PostInvoke(InvokeResult result);
    }
}