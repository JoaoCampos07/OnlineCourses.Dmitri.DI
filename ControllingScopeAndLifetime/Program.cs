using Autofac;
using System;

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

    public class SMSLog : ILog
    {
        private readonly string phoneNumber;

        public SMSLog(string phoneNumber)
        {
            this.phoneNumber = phoneNumber;
        }

        public void Dispose()
        {
        }

        public void Write(string msg) => Console.WriteLine($"SMS to {phoneNumber} : {msg}");
    }

    class Program
    {
        static void Main(string[] args)
        {
            // I want my components to life was long was I want
            var builder = new ContainerBuilder();
            builder.RegisterType<ConsoleLog>().As<ILog>()
                .SingleInstance(); // Everyt time I resolve a component as a service, I get a brand new Console Log, for example

            var container = builder.Build();

            // instead of using container.Resolve<ILog>()...we are going to restrain the Lifetime of the ILog
            using (var scope = container.BeginLifetimeScope())
            {
                var log = scope.Resolve<ILog>();
                log.Write("Testing!");

                var log2 = scope.Resolve<ILog>(); // I used the same instance was first command above
                log.Write("Testing!");
            }      
        }
    }
}
