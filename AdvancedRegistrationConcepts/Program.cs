using Autofac;
using Autofac.Core;
using System;

namespace AdvancedRegistrationConcepts
{
    public class Entity
    {
        public delegate Entity Factory();

        private static Random random = new Random();
        private int number;

        public Entity()
        {
            number = random.Next();
        }

        public override string ToString()
         => "test" + number.ToString();
    }

    public class ViewModel
    {
        // We are using DI container to inject a factory which is actually own by other class
        private readonly Entity.Factory factory;

        public ViewModel(Entity.Factory factory)
        {
            this.factory = factory;
        }

        // We want to construct a instante of Entity using DI
        public void Method()
        {
            var entity = factory.Invoke();
            Console.WriteLine(entity);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var cb = new ContainerBuilder();
            cb.RegisterType<Entity>().InstancePerDependency();
            cb.RegisterType<ViewModel>();

            var container = cb.Build();
            var vm = container.Resolve<ViewModel>();
            vm.Method();
            vm.Method();
        }
    }
}

