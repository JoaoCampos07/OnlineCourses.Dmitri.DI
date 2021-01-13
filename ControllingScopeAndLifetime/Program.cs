using Autofac;
using ControllingScopeAndLifetime;
using System;
using System.Collections.Generic;

namespace ControllingScopeAndLifetime
{
    public interface IResource : IDisposable
    {

    }

    public class SingletonResource : IResource
    {
        public SingletonResource()
        {
            Console.WriteLine("Instance per application lifetime Created");
        }

        public void Dispose()
        {
            Console.WriteLine("Instance per application lifetime destroyed");
        }
    }
    public class InstantPerDependencyResource : IResource
    {
        public InstantPerDependencyResource()
        {
            Console.WriteLine("Instance per dependency created");
        }

        public void Dispose()
        {
            Console.WriteLine("Instance per dependency destroyed");
        }
    }

    public class ResourceManager
    {
        public IEnumerable<IResource> Resources { get; set; }

        public ResourceManager(IEnumerable<IResource> resources)
        {
            Resources = resources;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var cb = new ContainerBuilder();
            cb.RegisterType<ResourceManager>().SingleInstance();
            cb.RegisterType<SingletonResource>().As<IResource>().SingleInstance();
            cb.RegisterType<InstantPerDependencyResource>().As<IResource>();

            using (var container = cb.Build())
            using (var scope = container.BeginLifetimeScope())
            {
                scope.Resolve<ResourceManager>();
            }

            // Because Resource Manager is a singleton it continues to have an hold on InstantPerDependencyResource (which is per request but remains for the lifetime of the app)
        }
    }
}
