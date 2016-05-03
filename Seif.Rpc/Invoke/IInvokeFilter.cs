namespace Seif.Rpc.Invoke
{
    public interface IInvokeFilter
    {
        IInvocation PreInvoke(IInvocation invocation);

        InvokeResult AfterInvoke(InvokeResult result);
    }
}