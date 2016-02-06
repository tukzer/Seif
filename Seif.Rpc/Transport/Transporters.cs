using System;
using System.Diagnostics.Contracts;
using System.Linq;
using Seif.Core;

namespace Seif.Rpc.Transport
{
    public class Transporters
    {
        public static IRpcServer Bind(string url, params IChannelHandler[] handlers)
        {
            return Bind(new Uri(url), handlers);
        }

        public static IRpcServer Bind(Uri uri, IChannelHandler[] handlers)
        {
            Contract.Requires(handlers != null && handlers.Any());

            IChannelHandler handler;
            if (handlers.Count() == 1)
            {
                handler = handlers[0];
            }
            else
            {
                handler = new ChannelHandlerDispatcher(handlers);
            }

            var transporter = GetTransporter();
            return transporter.Bind(uri, handler);
        }

        public static IRpcClient Connect(string url, params IChannelHandler[] handlers)
        {
            return Connect(new Uri(url), handlers);
        }

        private static IRpcClient Connect(Uri uri, IChannelHandler[] handlers)
        {
            IChannelHandler handler;
            if (!handlers.Any())
            {
                handler = new ChannelHandlerAdapter();
            }
            else if (handlers.Count() == 1)
            {
                handler = handlers[0];
            }
            else
            {
                handler = new ChannelHandlerDispatcher(handlers);
            }

            var transporter = GetTransporter();
            return transporter.Connect(uri, handler);
        }

        public static ITransporter GetTransporter()
        {
            return ApplicationContext.Get<ITransporter>();
        }
    }
}