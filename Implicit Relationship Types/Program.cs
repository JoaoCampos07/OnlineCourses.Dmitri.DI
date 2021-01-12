﻿using Autofac;
using Autofac.Features.OwnedInstances;
using System;

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
            private readonly Func<ConsoleLog> consoleLog;

            public Reporting(Func<ConsoleLog> consoleLog)
            {
                this.consoleLog = consoleLog;
            }

            public void Report()
            {
                consoleLog().Write("Reporting to console");
                consoleLog().Write("And Once again");
            }
        }

        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<ConsoleLog>();
            builder.RegisterType<Reporting>();
            using (var c = builder.Build()) // When Runtime leaves this using the objs forcely go to garbage automatically
            {
                c.Resolve<Reporting>().Report();
                Console.WriteLine("Done reporting.");
            }
        }
    }
}
