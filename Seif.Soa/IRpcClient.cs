using Seif.Core.Logging;
using Seif.Soa.Client.Filters;

namespace Seif.Soa
{
    public interface IRpcClient : IEndpoint
    {
        ILogFactory LogFactory { get; }

        void Refer<T>() where T : class ;

        IInvokeFilter[] InvokeFilters { get; }
    }
}