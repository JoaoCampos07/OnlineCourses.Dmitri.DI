using Autofac;
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

        public class Reporting
        {
            private readonly Meta<ConsoleLog> log;

            public Reporting(Meta<ConsoleLog> log)
            {
                this.log = log;
            }

            public void Report()
            {
                log.Value.Write("Starting report...");

                // What is the log level ? ...maybe extra operations are needed
                if (log.Metadata["mode"] as string == "verbose")
                    log.Value.Write($"VERBOSE MODE: Logger started on {DateTime.Now}");
            }
        }

        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<ConsoleLog>().WithMetadata("mode", "verbose");
            builder.RegisterType<Reporting>();
            using (var c = builder.Build()) // When Runtime leaves this using the objs forcely go to garbage automatically
            {
                c.Resolve<Reporting>().Report();
            }
        }
    }
}
