namespace Seif.Rpc.Client
{
    public class ProxyFactory : IProxyFactory
    {
        public T CreateInstance<T>()
        {
            return default(T);
        }


    }
}