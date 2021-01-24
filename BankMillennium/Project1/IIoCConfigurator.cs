namespace BM.Ioc.Abstractions
{
    using System;

    public interface IIocConfigurator
    {
        void RegisterTransient<TImplementation>() where TImplementation : class;

        void RegisterTransient<TInterface>(Func<TInterface> factory) where TInterface : class;

        void RegisterTransient<TInterface, TImplementation>()
            where TInterface : class
            where TImplementation : class, TInterface;

        void RegisterScoped<TInterface, TImplementation>()
           where TInterface : class
           where TImplementation : class, TInterface;

        void RegisterScoped<TImplementation>() where TImplementation : class;

        void RegisterScoped<TInterface>(Func<TInterface> factory) where TInterface : class;

        void RegisterSingleton<TInterface, TImplementation>()
            where TInterface : class
            where TImplementation : class, TInterface;

        void RegisterSingleton<TImplementation>() where TImplementation : class;

        void RegisterSingleton<TInterface>(Func<TInterface> factory) where TInterface : class;
    }
}
