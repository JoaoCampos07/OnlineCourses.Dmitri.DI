namespace BM.Ioc.Abstractions
{
    public interface IIocInitializer
    {
        void Initialize(IIocResolver resolver);
    }
}
