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

    class Program
    {
        static void Main(string[] args)
        {
            var b = new ContainerBuilder();
            b.RegisterType<ParentWithProp>();
            // Why this cmp cannot be register was a Instance per Dependency (default if not defined): 
            // Well, instance per depdency, is like injecting a brand new obj everytime is needed 
            // With Circular dependencies we can reach a Stack Overflow. 
            // Because everytime you create a Child for a Parent, and them a Parent for a child, everytime is a new obj so : 
            // Parent1 -> Child1 -> Parent2 -> Child2 etc...infinite loop
        }
    }
}
