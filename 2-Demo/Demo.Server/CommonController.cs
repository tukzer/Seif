using System.Web.Http;
using Seif.Rpc;
using Seif.Rpc.Default;
using Seif.Rpc.Invoke;

namespace Demo.Server
{
    public class CommonController : ApiController
    {
        private readonly ISerializer _serializer;

        public CommonController()
        {
            _serializer = SeifApplication.AppEnv.GlobalConfiguration.ProviderConfiguration.GetSerializer();
        }

        //public InvokeResult Post(RpcInvocation invocation)
        //{
        //    var invoker = new LocalInvoker();
        //    return invoker.Invoke(invocation);
        //}
        public InvokeResult Post([FromUri]string svc, [FromUri]string mtd, 
            [FromBody]string payload)
        {
            var rpcPayload = _serializer.Deserialize<RpcPayload>(payload) ?? new RpcPayload();

            var invoker = new LocalInvoker(_serializer);
            return invoker.Invoke(new RpcInvocation
            {
                Attributes = rpcPayload.Attributes,
                MethodName = mtd,
                ServiceName = svc,
                Parameters = rpcPayload.Parameters
            });
        }
        public InvokeResult Get(RpcInvocation invocation)
        {
            var invoker = new LocalInvoker(_serializer);
            return invoker.Invoke(invocation);
        }
    }
}
