using Autofac;
using ControllingScopeAndLifetime;
using System;
using System.Collections.Generic;
using System.Threading;

namespace ControllingScopeAndLifetime
{
    public class Parent
    {
        public override string ToString()
        => "I'm your father";
    }

    public class Child
    {
        public Child()
        {
            Console.WriteLine("Child obj created.");
        }

        public string Name { get; set; }
        public Parent Parent { get; set; } // Property Injection is needed here...

        public void SetParent(Parent parent)
            => Parent = parent;
    }

    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<Parent>();
            builder.RegisterType<Child>()
                .OnActivating(a =>
                {
                    Console.WriteLine("Child Activating");
                    a.Instance.Parent = a.Context.Resolve<Parent>();
                });
            // raised BEFORE the component child is use, NOT Build.

            using (var scope = builder.Build().BeginLifetimeScope())
            {
                var child = scope.Resolve<Child>();
                var parent = child.Parent;
                Console.WriteLine(parent.ToString());
            }
        }
    }
}
