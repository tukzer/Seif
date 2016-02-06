using System;
using Seif.Rpc.Transport;

namespace Seif.Rpc
{
    public class RpcException : ApplicationException
    {
        private readonly Uri _uri;
        private readonly string _msg;

        public RpcException(IChannel channel, string msg):
            this(channel.Url, msg)
        {
            
        }

        public RpcException(Uri uri, string msg)
        {
            _uri = uri;
            _msg = msg;
        }

        public Uri Uri
        {
            get { return _uri; }
        }

        public override string Message
        {
            get { return _msg; }
        }
    }
}