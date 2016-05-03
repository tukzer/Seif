using System;
using System.Runtime.Serialization;

namespace Seif.Rpc
{
    public class SeifException : ApplicationException
    {
        public SeifException()
        {
        }

        public SeifException(string message) : base(message)
        {
        }

        public SeifException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SeifException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}