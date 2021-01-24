using Autofac;
using Autofac.Core;
using Autofac.Core.Activators.Delegate;
using Autofac.Core.Lifetime;
using Autofac.Core.Registration;
using Autofac.Extras.AggregateService;
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
    public class Service4 : IService4 
    {
        private string _name;

        public Service4(string name)
        {
            _name = name;
        }
    }

    // Single interface that agregates all the services, exposing them was props
    public interface IMyAggregateService
    {
        IService1 Service1 { get; }
        IService2 Service2 { get; }
        IService3 Service3 { get; }
        // IService4 Service4 { get; } this is no longer valid, i need a method to expose IService4, and pass's field "name" that is needed

        IService4 GetService4(string name);
    }

    public class Consumer
    {
        public readonly IMyAggregateService _aggregateServices;

        public Consumer(IMyAggregateService service1)
        {
            _aggregateServices = service1;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var b = new ContainerBuilder();
            // How all the services that consumer obj needs are injected using the interface IMyAggreagateService ? 
            // R: A dynamic class will be created (using Castle.Core), implementing the IMyAggregateService 
            b.RegisterAggregateService<IMyAggregateService>();
            // register all those concrete class 
            b.RegisterAssemblyTypes(typeof(Program).Assembly)
                .Where(t => t.Name.StartsWith("Service"))
                .AsImplementedInterfaces();
            b.RegisterType<Consumer>();

            using (var c = b.Build())
            {
                var consumer = c.Resolve<Consumer>();
                Console.WriteLine(consumer._aggregateServices.GetService4("foo").GetType().Name);
            }

            // How this is impossible ? What is happenning ? 
            // R: Autofac nuget uses Castle.Core nuget. Castle core looks at the IMyAggregateService interface 
            //    and we finds all the services that are needed, and say "Let's make a concrete implementation of this interface...".
            //    So it creates a dynamic class/obj of that interface that exposes all members(services that are aggregate) and 
            //    them injects the appropriate type, exchanging services per components (IService1 for Service etc...)
            //    In the end, in the consumer class, i can just acess all the services i need using IMyAggregateService 
            //    knowing that i all have constrcuted objs and not Null references.
        }
    }
}
