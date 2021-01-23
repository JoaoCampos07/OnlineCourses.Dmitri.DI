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
    }

    public class ChildWithProp
    {
        public ParentWithProp Parent { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {

        }
    }
}
