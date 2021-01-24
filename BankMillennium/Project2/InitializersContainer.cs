//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="InitializersContainer.cs" company="Bank Millennium">
//    Copyright © Bank Millennium SA
//  </copyright>
//  <summary>
//    Defines the InitializersContainer.cs type.
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------
namespace BM.Infrastructure.Ioc.Autofac
{
    using System;
    using System.Collections.Generic;

    using BM.Ioc.Abstractions;

    internal class InitializersContainer
    {
        internal IIocResolver Resolver { get; }

        internal IEnumerable<IIocInitializer> Initializers { get; }

        public InitializersContainer(IEnumerable<IIocInitializer> initializers, IIocResolver resolver)
        {
            if (resolver == null )
            {
                throw new ArgumentNullException(nameof(resolver));
            }

            this.Resolver = resolver;
            this.Initializers = initializers ?? new IIocInitializer[0];
        }
    }
}