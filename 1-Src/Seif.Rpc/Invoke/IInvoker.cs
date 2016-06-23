namespace Seif.Rpc.Invoke
{
    public interface IInvoker
    {
        string Url { get;  }

        ISerializer Serializer { get; }

        InvokeResult Invoke(IInvocation invocation);
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