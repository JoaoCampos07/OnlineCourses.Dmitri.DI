using Autofac;
using Autofac.Features.OwnedInstances;
using System;
using System.Collections.Generic;

namespace Implicit_Relationship_Types
{
    class Program
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

        public class Reporting
        {
            private readonly IList<ILog> allLogs;

            public Reporting(IList<ILog> allLogs)
            {
                this.allLogs = allLogs;
            }

            public void Report()
            {
                foreach (var log in this.allLogs)
                {
                    Console.WriteLine($"Hello this is {log.GetType().Name}");
                }
            }
        }

        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<ConsoleLog>().As<ILog>();
            builder.Register(c => new SMSLog("+123456789")).As<ILog>();
            builder.RegisterType<Reporting>();
            using (var c = builder.Build()) // When Runtime leaves this using the objs forcely go to garbage automatically
            {
                c.Resolve<Reporting>().Report();
            }
        }
    }
}
