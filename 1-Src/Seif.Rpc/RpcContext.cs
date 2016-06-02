using System.Runtime.CompilerServices;

namespace Seif.Rpc
{
    public class RpcContext
    {
        private static RpcContext _instance;

        internal RpcContext()
        {
            
        }

        public static RpcContext Current
        {
            get { return _instance; }
        }



    }
}