using Seif.Soa.Client;

namespace Seif.Soa.Transport
{
    public interface ICodec
    {
        AbstractRequest Encode(IInvocation invocation);

        AbstractResponse Decode(IInvocation invocation);
    }
}