using System.Web.Http;
using Seif.Rpc.Invoke;
using Seif.Rpc.Invoke.Default;

namespace Demo.Server
{
    public class CommonController : ApiController
    {
        public InvokeResult Post(RpcInvocation invocation)
        {
            var invoker = new LocalInvoker();
            return invoker.Invoke(invocation);
        }
    }
}
