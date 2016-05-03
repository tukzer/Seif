using System;
using System.Collections.Generic;

namespace Seif.Rpc.Invoke
{
    public interface IInvoker
    {
        string Url { get;  }
        InvokeResult Invoke(IInvocation invocation);
    }

    public class InvokeResult
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public bool HasException { get; set; }
        public int Capacity { get; set; }
        public Exception[] Exceptions { get; set; }
        public object Result { get; set; }
    }

    public interface IInvocation
    {
        string TraceId { get; set; }
        ServiceKind ServiceKind { get; }
        string ServiceName { get; }
        string MethodName { get; }

        IDictionary<Type, object> Parameters { get; }
        IDictionary<string, string> Attributes { get; set; }
    }

    public class RpcInvocation : IInvocation
    {
        public virtual string ServiceUrl { get; set; }

        public virtual string TraceId { get; set; }

        public virtual ServiceKind ServiceKind { get; set; }

        public virtual string ServiceName { get; set; }

        public virtual string MethodName { get; set; }

        public virtual IDictionary<Type, object> Parameters { get; set; }

        public virtual IDictionary<string, string> Attributes { get; set; }
    }

    //public class LocalInvocation : IInvocation
    //{
    //    public string TraceId
    //    {
    //        get { throw new NotImplementedException(); }
    //        set { throw new NotImplementedException(); }
    //    }

    //    public ServiceKind ServiceKind
    //    {
    //        get { throw new NotImplementedException(); }
    //    }

    //    public string ServiceName
    //    {
    //        get { throw new NotImplementedException(); }
    //    }

    //    public string MethodName
    //    {
    //        get { throw new NotImplementedException(); }
    //    }

    //    public IDictionary<Type, object> Parameters
    //    {
    //        get { throw new NotImplementedException(); }
    //    }

    //    public IDictionary<string, string> Attributes
    //    {
    //        get
    //        {
    //            throw new NotImplementedException();
    //        }
    //        set
    //        {
    //            throw new NotImplementedException();
    //        }
    //    }
    //}

    //public class HttpInvocation : RpcInvocation
    //{
    //    public string HttpVerb { get; set; }
    //}
}