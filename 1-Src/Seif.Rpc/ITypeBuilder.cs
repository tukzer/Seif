namespace Seif.Rpc
{
    public interface ITypeBuilder
    {
        void RegisterType<T, TImpl>() where TImpl : class;
        void RegisterType<T, TImpl>(TImpl instance) where TImpl : class;

        void RegisterType<T>(T instance) where T : class ;

        void Build();

        T ResolveType<T>();
        object ResolveType(string interfaceName);
    }
}