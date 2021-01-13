using Autofac;
using Autofac.Features.Indexed;
using Autofac.Features.Metadata;
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

        public class Settings
        {
            public string LogMode { get; set; }
        }

        public class Reporting
        {
            private readonly IIndex<string, ILog> logs;

            public Reporting(IIndex<string, ILog> logs)
            {
                this.logs = logs;
            }

            public void Report()
            {
                logs["sms"].Write("Starting report output");
            }
        }

        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<ConsoleLog>().Keyed<ILog>("cmd");
            builder.Register(c => new SMSLog("+12345678")).Keyed<ILog>("sms");
            builder.RegisterType<Reporting>();

            using (var c = builder.Build())
            {
                c.Resolve<Reporting>().Report();
            }
        }
    }
}
