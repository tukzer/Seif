using System;
using System.Linq;
using System.Reflection;
using Seif.Rpc.Utils;

namespace Seif.Rpc.Invoke.Default
{
    public class LocalInvoker : IInvoker
    {
        public string Url
        {
            get { return SeifApplication.AppEnv.GlobalConfiguration.ConsumerConfiguration.Url; }
        }

        public InvokeResult Invoke(IInvocation invocation)
        {
            var instance = SeifApplication.ResolveByName(invocation.ServiceName);
            var type = instance.GetType();
            var methodInfo = type.GetMethod(invocation.MethodName,
                BindingFlags.CreateInstance | BindingFlags.Public);

            var invokeResult = new InvokeResult();
            try
            {
                var result = methodInfo.Invoke(instance, invocation.Parameters.Values.ToArray());
                invokeResult.Code = "200";
                invokeResult.Message = "调用成功";
                invokeResult.Result = result;
            }
            catch(Exception ex)
            {
                invokeResult.Code = "400";
                invokeResult.HasException = true;
                invokeResult.Exceptions = new[]{ex};
                invokeResult.Message = ex.Message;
            }

            return invokeResult;
        }
    }

    public class LocalInvoker<T> : IInvoker
    {
        public string Url
        {
            get { return SeifApplication.AppEnv.GlobalConfiguration.ProviderConfiguration.ApiDomain; }
        }

        public InvokeResult Invoke(IInvocation invocation)
        {
            var instance = SeifApplication.Resolve<T>();
            var methodInfo = typeof(T).GetMethod(invocation.MethodName,
                BindingFlags.CreateInstance | BindingFlags.Public);

            var invokeResult = new InvokeResult();
            try
            {
                var result = methodInfo.Invoke(instance, invocation.Parameters.Values.ToArray());
                invokeResult.Code = "200";
                invokeResult.Message = "调用成功";
                invokeResult.Result = result;
            }
            catch (Exception ex)
            {
                invokeResult.Code = "400";
                invokeResult.HasException = true;
                invokeResult.Exceptions = new[] { ex };
                invokeResult.Message = ex.Message;
            }

            return invokeResult;
        }
    }
}