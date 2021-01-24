using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace BM.Ioc.Abstractions
{
    [Obsolete]
    internal class IocResolverDecorator : IIocResolver
    {
        private readonly IServiceProvider _provider;

        public IocResolverDecorator(IServiceProvider provider)
        {
            this._provider = provider;
        }

        public T Resolve<T>() where T : class
        {
            return _provider.GetService<T>();
        }
    }
}
