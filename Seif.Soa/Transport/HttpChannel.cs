using System;

namespace Seif.Soa.Transport
{

    public class HttpChannel : IChannel
    {
        private readonly string _remoteAddress;
        private readonly ICodec _codec;
        private readonly ISerializer _serializer;

        public HttpChannel(string remoteAddress, ICodec codec, ISerializer serializer)
        {
            _remoteAddress = remoteAddress;
            _codec = codec;
            _serializer = serializer;
        }

        public string RemoteAddress
        {
            get { return _remoteAddress; }
        }

        public IChannelHandler Handler
        {
            get { throw new NotImplementedException(); }
        }

        public event Func<IChannel,AbstractRequest, bool> OnRequesting;
        public event Action<IChannel, AbstractResponse> OnSent;
        public event Action<IChannel, Exception> OnException;

        public IChannelHandler[] Handlers
        {
            get { throw new System.NotImplementedException(); }
        }

        public ISerializer Serializer
        {
            get { return _serializer; }
        }

        public ICodec Codec
        {
            get { return _codec; }
        }

        public AbstractResponse Send(AbstractRequest message)
        {
            throw new System.NotImplementedException();
        }


        public System.Uri Url
        {
            get
            {
                throw new System.NotImplementedException();
            }
        }

        public void Close()
        {
            throw new System.NotImplementedException();
        }
    }
}