//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ApplicationBuilderExtensions.cs" company="Bank Millennium">
//    Copyright © Bank Millennium SA
//  </copyright>
//  <summary>
//    Defines the ApplicationBuilderExtensions.cs type.
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------
namespace BM.Infrastructure.Ioc.Autofac
{
    using System;

    using BM.Ioc.Abstractions;

    using Microsoft.AspNetCore.Builder;

    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Initializes the modules impelments IIocInitializer,
        /// registered in ConfigureServices.CreateAutofacServiceProvider.
        /// </summary>
        /// <param name="applicationBuilder">The application builder.</param>
        /// <param name="initializers">The additional IIocInitializers.</param>
        public static void InitializeModules(this IApplicationBuilder applicationBuilder, params IIocInitializer[] initializers)
        {
            if (applicationBuilder == null)
            {
                throw new ArgumentNullException(nameof(applicationBuilder));
            }

            var initializersContainer = (InitializersContainer) applicationBuilder.ApplicationServices.GetService(typeof(InitializersContainer));

            if (initializersContainer != null)
            {
                foreach (var initializer in initializersContainer.Initializers)
                {
                    initializer.Initialize(initializersContainer.Resolver);
                }

                if (initializers != null)
                {
                    foreach (var initializer in initializers)
                    {
                        initializer.Initialize(initializersContainer.Resolver);
                    }
                }
            }
        }
    }
}