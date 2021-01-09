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

        public void SetParent(Parent parent)
            => Parent = parent;
    }

     class Program
    {
        static void Main(string[] args)
        {
            var cb = new ContainerBuilder();
            cb.RegisterType<Parent>();

            //Prop injection
            // 1 Way
            //cb.RegisterType<Child>().PropertiesAutowired(); // System is going to every prop and try to resolve it.

            // 2 Way 
            //cb.RegisterType<Child>()
            //    .WithProperty("Parent", new Parent()); //it will always be the same Parent for every child

            // Method injection
            // 3 Way
            cb.Register(c =>
            {
                var child = new Child();
                child.SetParent(c.Resolve<Parent>());
                return child;
            });

            var container = cb.Build();
            var parent = container.Resolve<Child>().Parent;
            Console.WriteLine(parent);
        }
    }
}

