using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace BM.Ioc.Abstractions
{
    [Obsolete]
    public static class IServiceProviderExtensions
    {
        [Obsolete]
        public static void InitializeModules(this IServiceProvider serviceProvider)
        {
            var initializers = serviceProvider.GetServices<IIocInitializer>();
            var decorator = new IocResolverDecorator(serviceProvider);
            foreach (var item in initializers)
            {
                item.Initialize(decorator);
            }
        }
    }
}
