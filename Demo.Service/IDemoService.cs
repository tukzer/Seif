namespace Demo.Service
{
    public interface IDemoService
    {
        void PrintServer();

        int AddModel(ComplexModel model);

        ComplexModel GetModel(string name);
        ComplexModel GetModel(string name, string value);
    }
}