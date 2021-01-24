using Autofac;
using Autofac.Core;
using Autofac.Core.Activators.Delegate;
using Autofac.Core.Lifetime;
using Autofac.Core.Registration;
using Autofac.Extras.AttributeMetadata;
using Autofac.Features.AttributeFilters;
using Autofac.Features.Metadata;
using Autofac.Features.ResolveAnything;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace AdvancedTopics
{
    // Agreggate Services : Treat mulitple services was only one service using Autofac

    public interface IService1 { }
    public interface IService2 { }
    public interface IService3 { }
    public interface IService4 { }

    public class Service1 : IService1 { }
    public class Service2 : IService2 { }
    public class Service3 : IService3 { }
    public class Service4 : IService4 { }

    public class Consumer
    {
        private readonly IService1 _service1;
        private readonly IService2 _service2;
        private readonly IService3 _service3;
        private readonly IService4 _service4;

        public Consumer(IService1 service1, IService2 service2, IService3 service3, IService4 service4)
        {
            _service1 = service1;
            _service2 = service2;
            _service3 = service3;
            _service4 = service4;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var b = new ContainerBuilder();

            using (var c = b.Build())
            {
            }
        }
    }
}
