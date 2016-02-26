using System;

namespace Seif.Soa
{
    public abstract class InvokeResult
    {
        protected InvokeResult()
        {

        }

        protected InvokeResult(Exception exception)
        {
            Exception = exception;
        }


        public bool HasException
        {
            get { return Exception != null; }
        }

        public Exception Exception { get; protected set; }
    }
}