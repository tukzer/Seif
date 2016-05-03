namespace Seif.Rpc
{
    public interface ITypeBuilder
    {
        void RegisterType<T>();
        void RegisterType<T>(T instance);

        T ResolveType<T>();
    }
}