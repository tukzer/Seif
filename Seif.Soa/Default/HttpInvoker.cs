using Seif.Soa.Proxy;

namespace Seif.Soa.Default
{
    public class HttpInvoker : IInvoker
    {

        public System.Type ServiceType
        {
            get { throw new System.NotImplementedException(); }
        }

        public InvokeResult Invoke(Client.IInvocation invocation)
        {
            var rpcInvo = invocation as RpcInvocation;

            if (rpcInvo != null)
            {
                
            }
        }


    }
}