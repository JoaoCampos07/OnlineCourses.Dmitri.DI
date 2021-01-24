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

    // Single interface that agregates all the services, exposing them was props
    public interface IMyAggregateService
    {
        IService1 Service1 { get; }
        IService2 Service2 { get; }
        IService3 Service3 { get; }
        IService4 Service4 { get; }
    }

    public class Consumer
    {
        public readonly IMyAggregateService _service1;

        public Consumer(IMyAggregateService service1)
        {
            _service1 = service1;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var b = new ContainerBuilder();
            // How all the services that consumer obj needs are injected using the interface IMyAggreagateService ? 


            using (var c = b.Build())
            {
            }
        }
    }
}
