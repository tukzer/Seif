﻿using System;

namespace Seif.Rpc.Transport
{
    public class ChannelHandlerDispatcher : IChannelHandler
    {
        public ChannelHandlerDispatcher(IChannelHandler[] handlers)
        {
            throw new NotImplementedException();
        }

        public void OnConnected(IChannel channel)
        {
            throw new NotImplementedException();
        }

        public void OnDisconnected(IChannel channel)
        {
            throw new NotImplementedException();
        }

        public void OnSent(IChannel channel, object message)
        {
            throw new NotImplementedException();
        }

        public void OnReceived(IChannel channel, object message)
        {
            throw new NotImplementedException();
        }

        public void OnException(IChannel channel, Exception ex)
        {
            throw new NotImplementedException();
        }
    }
}