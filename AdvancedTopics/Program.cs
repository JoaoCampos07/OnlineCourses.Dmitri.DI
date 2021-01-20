using Autofac;
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

            using (var c = b.Build())
            {
                c.Resolve<Person>().Speak();
            }
        }
    }
}
