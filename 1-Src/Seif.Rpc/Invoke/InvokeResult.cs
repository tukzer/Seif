using System;

namespace Seif.Rpc.Invoke
{
    public class InvokeResult
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public bool HasException { get; set; }
        public int Capacity { get; set; }
        public Exception[] Exceptions { get; set; }
        public object Result { get; set; }
    }
}