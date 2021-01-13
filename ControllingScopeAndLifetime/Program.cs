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
            var builder = new ContainerBuilder();
            builder.RegisterType<ConsoleLog>()
                .As<ILog>()
                .OnActivating(a =>
                {
                    a.ReplaceInstance(new SMSLog("+123456789"));
                });
                    
            using (var scope = builder.Build().BeginLifetimeScope())
            {
                var log = scope.Resolve<ILog>();
                log.Write("Test");
            }
        }
    }
}
