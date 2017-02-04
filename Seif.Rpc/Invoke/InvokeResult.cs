using System;
using System.Linq;

namespace Seif.Rpc.Invoke
{
    public class InvokeResult
    {
        public ResultStatus Status { get; set; }
        public string Message { get; set; }

        public bool HasException
        {
            get { return Exceptions != null && Exceptions.Any(); }
        }

        public int Capacity { get; set; }
        public Exception[] Exceptions { get; set; }
        public object Result { get; set; }
    }

    public enum ResultStatus
    {
        Success = 200,
        BusinessError = 400,
        FrameworkError = 500,
        ServerNotReachable = 404,
        UnknownError = 501
    }
}