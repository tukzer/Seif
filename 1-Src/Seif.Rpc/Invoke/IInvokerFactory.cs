using Seif.Rpc.Registry;

namespace Seif.Rpc.Invoke
{
    public interface IInvokerFactory
    {
        IInvoker CreateInvoker(ServiceRegistryMetta options);
    }

    //public class InvokeOptions
    //{
    //    public string ServiceType { get; set; }
    //    public string ServiceUrl { get; set; }
    //    public string Verb { get; set; }
    //}
}