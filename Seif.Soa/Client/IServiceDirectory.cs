namespace Seif.Soa.Client
{
    public interface IServiceDirectory<T>
    {
        T ServiceType { get; set; }

        IInvoker[] Discovery();
    }
}