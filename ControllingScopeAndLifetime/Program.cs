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
                .InstancePerMatchingLifetimeScope("foo");

            var container = builder.Build();

            // instead of using container.Resolve<ILog>()...we are going to restrain the Lifetime of the ILog
            using (var scope1 = container.BeginLifetimeScope("foo"))
            {
                for (int i = 0; i < 3; i++)
                {
                    scope1.Resolve<ILog>(); // just one console log obj 
                }

                using (var scope2 = scope1.BeginLifetimeScope())
                {
                    for (int i = 0; i < 3; i++)
                    {
                        scope2.Resolve<ILog>();  // just one console log inside Matthcing Lifetime, even if we have nested Lifetime
                    }
                }
            }

            // This will give error, because there is no TAG to do matching
            //using (var scope3 = container.BeginLifetimeScope())
            //{
            //    scope3.Resolve<ILog>();
            //}
        }
    }
}
