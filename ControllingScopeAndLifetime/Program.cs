using Autofac;
using ControllingScopeAndLifetime;
using System;
using System.Collections.Generic;
using System.Threading;

namespace ControllingScopeAndLifetime
{
    // Call something(a component) when COntainer is being build...
    public class StartLog : IStartable
    {
        public StartLog()
        {
            Console.WriteLine("Startlog constructured");
        }
        public void Start()
        {
            Console.WriteLine("Container being built"); ;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<StartLog>()
                .AsSelf()
                .As<IStartable>()
                .SingleInstance();

            var container = builder.Build();
        }
    }
}
