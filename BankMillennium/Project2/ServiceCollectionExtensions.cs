//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ServiceCollectionExtensions.cs" company="Bank Millennium">
//    Copyright © Bank Millennium SA
//  </copyright>
//  <summary>
//    Defines the ServiceCollectionExtensions.cs type.
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------
namespace BM.Infrastructure.Ioc.Autofac
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using BM.Ioc.Abstractions;

    using global::Autofac;
    using global::Autofac.Extensions.DependencyInjection;

    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceCollectionExtensions
    {
        public static IServiceProvider CreateAutofacServiceProvider(this IServiceCollection serviceCollection, params IIocModule[] modules)
        {
            if (serviceCollection == null)
            {
                throw new ArgumentNullException(nameof(serviceCollection));
            }

            IContainer container;
            return serviceCollection.CreateAutofacServiceProvider(out container, modules);
        }

        public static IServiceProvider CreateAutofacServiceProvider(this IServiceCollection serviceCollection, Action<IContainer> configureContainer, params IIocModule[] modules)
        {
            if (serviceCollection == null)
            {
                throw new ArgumentNullException(nameof(serviceCollection));
            }

            IContainer container;
            var provider = serviceCollection.CreateAutofacServiceProvider(out container, modules);
            configureContainer?.Invoke(container);
            return provider;
        }

        public static IServiceProvider CreateAutofacServiceProvider(this IServiceCollection serviceCollection, out IContainer container, params IIocModule[] modules)
        {
            if (serviceCollection == null)
            {
                throw new ArgumentNullException(nameof(serviceCollection));
            }

            var builder = new ContainerBuilder();
            var configurator = new AutofacIoCConfigurator(builder);

            // register modules
            RegisterModules(configurator, modules);
            builder.Populate(serviceCollection);

            // register initializers for future processing 
            var initializersContainer = new InitializersContainer(modules?.Select(x => x as IIocInitializer).Where(x => x != null), configurator);
            configurator.RegisterSingleton(() => initializersContainer);

            container = builder.Build();

            configurator.SetContainer(container);

            return new AutofacServiceProvider(container); ;
        }

        private static void RegisterModules(IIocConfigurator configurator, IEnumerable<IIocModule> modules)
        {
            modules?
                .ToList()
                .ForEach(module => module.Configure(configurator));
        }
    }
}