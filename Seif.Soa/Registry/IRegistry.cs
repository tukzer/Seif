namespace Seif.Soa.Registry
{
    public interface IRegistry
    {
        string Name { get; }

        string Url { get; }

        IInvoker<T> GetInvokers<T>() where T : class ;
    }
}