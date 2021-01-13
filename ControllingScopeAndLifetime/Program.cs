using Autofac;
using ControllingScopeAndLifetime;
using System;
using System.Collections.Generic;
using System.Threading;

namespace ControllingScopeAndLifetime
{
    public interface ILog : IDisposable
    {
        void Write(string msg);
    }

    public class ConsoleLog : ILog
    {
        public ConsoleLog()
        {
            Console.WriteLine($"Console Log create at {DateTime.Now.Ticks}");
        }

        public void Dispose()
        {
            Console.WriteLine("Console Log no longer required,"); // This will tell us when the component is throw out by GC
        }

        public void Write(string msg) => Console.WriteLine(msg);
    }
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            //builder.RegisterType<ConsoleLog>();
            builder.RegisterInstance(new ConsoleLog());

            var container = builder.Build();
            using (var scope = container.BeginLifetimeScope())
            {
                var x  = scope.Resolve<ConsoleLog>();
                // I managed to dispose the obj somehow ehere
            }

            using (var scope2 = container.BeginLifetimeScope())
            {
                // I would have a disposed obj here...
                var x = scope2.Resolve<ConsoleLog>();
            }
        }
    }
}
