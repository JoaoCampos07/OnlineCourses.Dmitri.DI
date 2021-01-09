using Autofac;
using Autofac.Core;
using System;

namespace AdvancedRegistrationConcepts
{
    public class Service
    {
        public string DoSomething(int value)
            => $"I have a {value}";
    }

    public class DomainObject
    {
        private Service service;
        private int value;

        public delegate DomainObject Factory(int value); // we specify params that are NOT auto injected by AutoFac 

        public DomainObject(Service service, int value)
        {
            this.service = service;
            this.value = value;
        }

        public override string ToString()
        => this.service.DoSomething(this.value);
    }

    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<Service>();
            builder.RegisterType<DomainObject>();

            var container = builder.Build();
            // Passed Arguments to a obj when we are resolving it.
            // 1 Way : 
            // ( I want to provide a value right here )

            //var obj = container.Resolve<DomainObject>(new PositionalParameter(1, 42)); // 1 -> because the parameter is at position 1 in the constructor.
            //Console.WriteLine(obj.ToString());

            // 2 Way : Instead of resolving the obj we resolve the facotry
            // Resolve delegate factory that makes uses of container and Injects both dependency objs but also params not auto injected
            var factory = container.Resolve<DomainObject.Factory>(); 
            var obj = factory.Invoke(42);
            Console.WriteLine(obj.ToString());
        }
    }
}
