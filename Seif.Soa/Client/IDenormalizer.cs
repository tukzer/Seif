using Seif.Soa.Transport;

namespace Seif.Soa.Client
{
    public interface IDenormalizer
    {
        AbstractRequest Denormalize(IInvocation invocation);
    }
}