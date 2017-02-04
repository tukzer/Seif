using System.Threading.Tasks;

namespace Seif.Rpc.Runtime
{
    public interface IServiceHost : IServiceEndpoint
    {
        Task StartAsync();
    }
}