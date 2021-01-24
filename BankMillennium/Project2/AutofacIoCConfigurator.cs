namespace BM.Infrastructure.Ioc.Autofac
{
    using System;

    using BM.Ioc.Abstractions;

    using global::Autofac;

    public class AutofacIoCConfigurator : IIocConfigurator, IIocResolver
    {
        private readonly ContainerBuilder containerBuilder;

        private IContainer container;

        public AutofacIoCConfigurator(ContainerBuilder containerBuilder)
        {
            this.containerBuilder = containerBuilder;
        }

        public void RegisterTransient<TImplementation>() where TImplementation : class
        {
            this.containerBuilder.RegisterType<TImplementation>().AsSelf();
        }

        public void RegisterTransient<TInterface>(Func<TInterface> factory) where TInterface : class
        {
            this.containerBuilder.Register(context => factory()).AsSelf();
        }

        public void RegisterTransient<TInterface, TImplementation>() where TInterface : class where TImplementation : class, TInterface
        {
            this.containerBuilder.RegisterType<TImplementation>().As<TInterface>();
        }

        public void RegisterScoped<TInterface, TImplementation>() where TInterface : class where TImplementation : class, TInterface
        {
            this.containerBuilder.RegisterType<TImplementation>().As<TInterface>().InstancePerLifetimeScope();
        }

        public void RegisterScoped<TImplementation>() where TImplementation : class
        {
            this.containerBuilder.RegisterType<TImplementation>().AsSelf().InstancePerLifetimeScope();
        }

        public void RegisterScoped<TInterface>(Func<TInterface> factory) where TInterface : class
        {
            this.containerBuilder.Register(context => factory()).AsSelf().InstancePerLifetimeScope();
        }

        public void RegisterSingleton<TInterface, TImplementation>() where TInterface : class where TImplementation : class, TInterface
        {
            this.containerBuilder.RegisterType<TImplementation>().As<TInterface>().SingleInstance();
        }

        public void RegisterSingleton<TImplementation>() where TImplementation : class
        {
            this.containerBuilder.RegisterType<TImplementation>().AsSelf().SingleInstance();
        }

        public void RegisterSingleton<TInterface>(Func<TInterface> factory) where TInterface : class
        {
            this.containerBuilder.Register(context => factory()).AsSelf().SingleInstance();
        }

        public T Resolve<T>() where T : class
        {
            return this.container.Resolve<T>();
        }

        public void SetContainer(IContainer buildedContainer)
        {
            this.container = buildedContainer;
        }
    }
}
