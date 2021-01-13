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
            Console.WriteLine("Good Child obj created.");
        }

        public string Name { get; set; }
        public Parent Parent { get; set; } // Property Injection is needed here...

        public void SetParent(Parent parent)
            => Parent = parent;

        public override string ToString()
        {
            return "I love you";
        }
    }

    public class BadChild : Child
    {
        public BadChild()
        {
            Console.WriteLine("Bad Child obj created.");
        }

        public string Name { get; set; }
        public Parent Parent { get; set; } // Property Injection is needed here...

        public void SetParent(Parent parent)
            => Parent = parent;

        public override string ToString()
        {
            return "I hate you";
        }
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
                    //a.Instance.Parent = a.Context.Resolve<Parent>();

                    a.ReplaceInstance(new BadChild());
                })
                .OnActivated(a =>
                {
                    Console.WriteLine("Child Activated");
                })
                // raised AFTER the component child is fully constructed
                .OnRelease(a =>
                {
                    Console.WriteLine("Child about to be removed");
                });

            using (var scope = builder.Build().BeginLifetimeScope())
            {
                var child = scope.Resolve<Child>();
                var parent = child.Parent;
                Console.WriteLine(child);
                Console.WriteLine(parent);
            }
        }
    }
}
