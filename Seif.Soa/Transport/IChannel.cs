using System;

namespace Seif.Soa.Transport
{
    /// <summary>
    /// 代表一个处理通道，如一个可用的服务器处理通道，如WebAPI通道。
    /// 一条处理的参与这(Serializer, Transport, Deserializer)
    /// 调用链：Assembly, Service, Method
    /// </summary>
    public interface IChannel : IEndpoint
    {
        #region Events

        event Func<IChannel,AbstractRequest, bool> OnRequesting;

        event Action<IChannel, AbstractResponse> OnSent;

        event Action<IChannel, Exception> OnException;

        #endregion


        /// <summary>
        /// 可用的处理逻辑
        /// </summary>
        IChannelHandler[] Handlers { get;  }

        /// <summary>
        /// 可用的序列化工具
        /// </summary>
        ISerializer Serializer { get; }

        /// <summary>
        /// 编码组包逻辑
        /// </summary>
        ICodec Codec { get; }

        /// <summary>
        /// 发送请求
        /// </summary>
        /// <param name="message">请求数据</param>
        /// <returns>请求结果</returns>
        AbstractResponse Send(AbstractRequest message);
    }
}