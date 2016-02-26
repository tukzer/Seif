namespace Seif.Soa.Proxy
{
    public interface IProxyFactory
    {
        T CreateProxy<T>( string url) where T : class ;
    }
}