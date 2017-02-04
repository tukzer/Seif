namespace Seif.Rpc.Invoke
{
    public class PooledInvokerFactory : IInvokerFactory
    {
        public IInvoker CreateInvoker<T>(string url, InvokeOptions options)
        {
            throw new System.NotImplementedException();
        }

        public IInvoker CreateInvoker<T>(Dispatch.IDispatcher dispatcher, InvokerInstanceType instanceType, InvokeOptions options)
        {
            throw new System.NotImplementedException();
        }
    }
}