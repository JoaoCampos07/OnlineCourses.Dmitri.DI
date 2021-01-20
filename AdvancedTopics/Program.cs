using Autofac;
using Autofac.Features.ResolveAnything;
using System;

namespace AdvancedTopics
{
    public interface ICanSpeak
    {
        void Speak();
    }

    public class Person : ICanSpeak
    {
        public void Speak()
        {
            Console.WriteLine("Hello");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var b = new ContainerBuilder();
            b.RegisterSource(new AnyConcreteTypeNotAlreadyRegisteredSource());

            using (var c = b.Build())
            {
                c.Resolve<Person>().Speak();
            }
        }
    }
}
