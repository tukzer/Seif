using System;
using System.Linq;
using System.Reflection;
using Seif.Rpc.Common;
using Seif.Rpc.Invoke;
using Seif.Rpc.Utils;

namespace Seif.Rpc.Default
{
    public class LocalInvoker : IInvoker
    {
        private readonly ISerializer _serializer;

        public LocalInvoker(ISerializer serializer)
        {
            _serializer = serializer;
        }

        public string Url
        {
            get { return SeifApplication.AppEnv.GlobalConfiguration.ConsumerConfiguration.Url; }
        }

        public ISerializer Serializer
        {
            get { return _serializer; }
        }

        public InvokeResult Invoke(IInvocation invocation)
        {
            var instance = SeifApplication.ResolveByName(invocation.ServiceName);
            var type = instance.GetType();

            //var parameters = TypeUtils.ConvertTypeMap(_serializer, invocation.Parameters);
            var parameterExts = invocation.Parameters.Select(p => ParameterExt.From(p, _serializer)).ToList();

            var methodInfo = type.GetMethod(invocation.MethodName,
                BindingFlags.Instance | BindingFlags.Public, null, parameterExts.Select(p => p.Type).ToArray(), new ParameterModifier[0]);

            var invokeResult = new InvokeResult();
            try
            {
                var result = methodInfo.Invoke(instance, parameterExts.Select(p => p.Value).ToArray());
                invokeResult.Status= ResultStatus.Success;
                invokeResult.Message = "调用成功";
                invokeResult.Result = _serializer.Serialize(result);
            }
            catch(Exception ex)
            {
                invokeResult.Status = ResultStatus.UnknownError;
                invokeResult.Exceptions = new[]{ex};
                invokeResult.Message = ex.Message;
            }

            return invokeResult;
        }
    }

    //public class LocalInvoker<T> : IInvoker
    //{
    //    private readonly ISerializer _serializer;

    //    public LocalInvoker(ISerializer serializer)
    //    {
    //        _serializer = serializer;
    //    }

    //    public string Url
    //    {
    //        get { return SeifApplication.AppEnv.GlobalConfiguration.ProviderConfiguration.ApiDomain; }
    //    }

    //    public ISerializer Serializer
    //    {
    //        get { return _serializer; }
    //    }

    //    public InvokeResult Invoke(IInvocation invocation)
    //    {
    //        var instance = SeifApplication.Resolve<T>();
    //        var methodInfo = typeof(T).GetMethod(invocation.MethodName,
    //            BindingFlags.CreateInstance | BindingFlags.Public);

    //        var invokeResult = new InvokeResult();
    //        var parameters = TypeUtils.ConvertTypeMap()

    //        try
    //        {
    //            var result = methodInfo.Invoke(instance, invocation.Parameters.Values.ToArray());
    //            invokeResult.Status = ResultStatus.Success;
    //            invokeResult.Message = "调用成功";
    //            invokeResult.Result = result;
    //        }
    //        catch (Exception ex)
    //        {
    //            invokeResult.Status = ResultStatus.UnknownError;
    //            invokeResult.Exceptions = new[] { ex };
    //            invokeResult.Message = ex.Message;
    //        }

    //        return invokeResult;
    //    }
    //}
}