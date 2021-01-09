using Autofac;
using Autofac.Core;
using System;

namespace AdvancedRegistrationConcepts
{
    public class Parent
    {
        public override string ToString()
        => "I'm your father";
    }

    public class Child
    {
        public string Name { get; set; }
        public Parent Parent { get; set; }
    }

     class Program
    {
        static void Main(string[] args)
        {
            var cb = new ContainerBuilder();
            cb.RegisterType<Parent>();

            // 1 Way
            //cb.RegisterType<Child>().PropertiesAutowired(); // System is going to every prop and try to resolve it.

            // 2 Way 
            cb.RegisterType<Child>()
                .WithProperty("Parent", new Parent());

            var container = cb.Build();
            var parent = container.Resolve<Child>().Parent;
            Console.WriteLine(parent);
        }
    }
}

