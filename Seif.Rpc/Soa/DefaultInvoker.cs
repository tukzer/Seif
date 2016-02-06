using System.Linq;
using System.Reflection;
using Seif.Core;
using Seif.Rpc.Properties;

namespace Seif.Rpc.Soa
{
    public class DefaultInvoker<T> : IInvoker<T> where T : class
    {
        public T ServiceInterface
        {
            get { throw new System.NotImplementedException(); }
        }

        public RpcResult<T> Invoke(IInvocation invocation)
        {
            var interfaceInstance = ApplicationContext.Get<T>();

            var interfaceType = typeof (T);
            var methodInfo = interfaceType.GetMethod(invocation.MethodName,
                BindingFlags.Instance | BindingFlags.InvokeMethod | BindingFlags.Public, null,
                invocation.Parameters.Keys.ToArray(), new ParameterModifier[0]);

            var result = methodInfo.Invoke(interfaceInstance, invocation.Parameters.Values.ToArray());
            var serializedResult = (T) result;
            return new RpcResult<T>(serializedResult);
        }
    }
}