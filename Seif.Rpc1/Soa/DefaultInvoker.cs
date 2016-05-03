using System;
using System.Linq;
using System.Reflection;
using Seif.Core;
using Seif.Rpc.Properties;
using Seif.Rpc.Server;

namespace Seif.Rpc.Soa
{
    /// <summary>
    /// 通过<see cref="IInvocation"/>参数查找暴漏的服务方法，并执行调用，返回结果
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DefaultInvoker<T> : IInvoker<T> where T : class
    {
        public T ServiceInterface
        {
            get { throw new NotImplementedException(); }
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

        private Type[] ServiceTypes
        {
            get
            {
                var assembly = Assembly.GetExecutingAssembly();
                return assembly.GetExportedTypes().Where(p => p.IsInterface).ToArray();
            }
        }

        private T GetProxyType(IInvocation invocation)
        {
            var interfaceType = ServiceTypes.FirstOrDefault(p => p.Name == invocation.ServiceName);
            if(interfaceType == null)
                throw new RpcException(RpcContext.Current.RequestUri, "错误的接口名或分发有误");

            var method = interfaceType.GetMethod(invocation.MethodName,
                BindingFlags.Instance | BindingFlags.InvokeMethod | BindingFlags.Public, null,
                invocation.Parameters.Keys.ToArray(), new ParameterModifier[0]);

            var result = method.Invoke(null, invocation.Parameters.Values.ToArray());
            return (T)result;
        }
    }
}