using Seif.Rpc.Dispatch;
using Seif.Rpc.Proxy;

namespace Seif.Rpc.Invoke
{
    /// <summary>
    /// 生成代理IProxyFactory=》调用代理Call=》拉取服务列表IServiceRegistry=》分发IDispatcher=》生成Invoker IInvokerFactory=》调用Invoker IInvoker
    /// IInvokerFactory决定是否每次都创建实例
    /// </summary>
    public interface IInvokerFactory
    {
        /// <summary>
        /// 生成一个给定的代理
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        IInvoker CreateInvoker<T>(string url, InvokeOptions options);

        /// <summary>
        /// 根据接口类型和负载均衡情况，生成或获取一个代理
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dispatcher"></param>
        /// <param name="instanceType"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        IInvoker CreateInvoker<T>(IDispatcher dispatcher, InvokerInstanceType instanceType, InvokeOptions options);
    }
}

