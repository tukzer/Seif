using Seif.Rpc.Properties;

namespace Seif.Rpc.Soa
{
    public interface ISoaRouter
    {
        string Export<T>(IInvoker<T> invoker);

        IInvoker<T> Refer<T>(string url);
    }
}