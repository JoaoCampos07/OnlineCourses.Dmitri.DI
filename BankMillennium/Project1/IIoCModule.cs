namespace BM.Ioc.Abstractions
{
    public interface IIocModule
    {
        void Configure(IIocConfigurator configurator);
    }
}
