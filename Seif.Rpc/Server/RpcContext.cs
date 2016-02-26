using System;
using System.Threading;
using Seif.Rpc.Properties;

namespace Seif.Rpc.Server
{
    public class RpcContext
    {
         private static readonly ThreadLocal<RpcContext>  LocalContext = new ThreadLocal<RpcContext>(() => new RpcContext());

        private RpcContext()
        {
            
        }

        public static RpcContext Current {
            get { return LocalContext.Value; }
        }

        public void Clear()
        {
            LocalContext.Dispose();
        }

        public Uri RequestUri { get; set; }

        public IInvocation Invocation { get; set; }
        //public RpcResult<T> Result { get; set; }
        //public IInvoker<T> Invokers { get; set; }
    }
}