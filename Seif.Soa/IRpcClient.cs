using Seif.Core.Logging;
using Seif.Soa.Client.Filters;
using Seif.Soa.Configuration;

namespace Seif.Soa
{
    public interface IRpcClient : IEndpoint
    {
        ILogFactory LogFactory { get; }

        void Init(RegistryConfiguaration registry);

        void Refer<T>() where T : class ;

        IInvokeFilter[] InvokeFilters { get; }
    }
}