using Seif.Rpc.Invoke;

namespace Seif.Rpc.Dispatch
{
    public interface IInvokeDispatcher
    {
        IInvoker Dispatch<T>(DispatchOptions options);
    }

    public class DispatchOptions
    {
        public ServiceKind ServiceKind { get; set; }
        public IDispathStragedy Stragedy { get; set; }
    }
}