using Autofac;
using Autofac.Core;
using System;

namespace AdvancedRegistrationConcepts
{
    public class Entity
    {
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
        private readonly IContainer container;

        public ViewModel(IContainer container)
        {
            this.container = container;
        }

        // We want to construct a instante of Entity using DI
        public void Method()
        {
            // this usually wrong...
            var entity = container.Resolve<Entity>();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {

        }
    }
}

