using Autofac;
using Autofac.Core;
using Autofac.Core.Activators.Delegate;
using Autofac.Core.Lifetime;
using Autofac.Core.Registration;
using Autofac.Features.Metadata;
using Autofac.Features.ResolveAnything;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace AdvancedTopics
{
    // Obj1 with Prop injection and Obj2 with Prop injection
    public class ParentWithProp
    {
        public ChildWithProp Child { get; set; }

        public override string ToString() => "Parent";
    }

    public class ChildWithProp
    {
        public ParentWithProp Parent { get; set; }

        public override string ToString() => "Child";
    }

    // Obj1 with Constructor injection and Obj2 with Prop injection
    public class ParentWithConstructor1
    {
        public ChildWithProperty1 Child { get; set; }

        public ParentWithConstructor1(ChildWithProperty1 child)
        {
            Child = child;
        }

        public override string ToString() => "Parent with Child with Property.";
    }

    public class ChildWithProperty1
    {
        public ParentWithConstructor1 Parent { get; set; }

        public override string ToString() => "Child with Parent with constuctor.";
        
    }

    class Program
    {
        static void Main(string[] args)
        {
            var b = new ContainerBuilder();
            b.RegisterType<ParentWithConstructor1>().InstancePerLifetimeScope();
            b.RegisterType<ChildWithProperty1>()
                .InstancePerLifetimeScope()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);

            using (var c = b.Build())
                Console.WriteLine(c.Resolve<ParentWithConstructor1>().Child.Parent);
        }

        static void Main_(string[] args)
        {
            var b = new ContainerBuilder();
            b.RegisterType<ParentWithProp>()
                .InstancePerLifetimeScope() // shares a instance like this...
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
            // Why this cmp cannot be register was a Instance per Dependency (default if not defined): 
            // Well, instance per depdency, is like injecting a brand new obj everytime is needed 
            // With Circular dependencies we can reach a Stack Overflow. 
            // Because everytime you create a Child for a Parent, and them a Parent for a child, everytime is a new obj so : 
            // Parent1 -> Child1 -> Parent2 -> Child2 etc...infinite loop

            b.RegisterType<ChildWithProp>()
                .InstancePerLifetimeScope()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);

            using (var c = b.Build())
            {
                Console.WriteLine(c.Resolve<ParentWithProp>().Child.Parent);
            }
        }
    }
}
