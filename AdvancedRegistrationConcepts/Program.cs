using Autofac;
using Autofac.Core;
using System;

namespace AdvancedRegistrationConcepts
{
    public interface ILog
    {
        void Write(string message);
    }
    public class ConsoleLog : ILog
    {
        public void Write(string message)
        {
            Console.WriteLine(message);
        }
    }

    public class EmailLog : ILog
    {
        private const string adminEmail = "admin@foo.com";
        public void Write(string message)
        {
            Console.WriteLine($"Email sent to : {adminEmail} : {message}");
        }
    }

    public class SMSLog : ILog
    {
        string phoneNumber;

        public SMSLog(string phoneNumber)
        {
            this.phoneNumber = phoneNumber;
        }

        public void Write(string message)
        {
            Console.WriteLine($"SMS to {phoneNumber} : {message}");
        }
    }

    public class Engine
    {
        private ILog log;
        private int id;

        public Engine(ILog log)
        {
            this.log = log;
            this.id = new Random().Next();
        }

        public Engine(ILog log, int id)
        {
            this.log = log;
            this.id = id;
        }

        public void Ahead(int power)
        {
            log.Write($"Engine [{id}] ahead {power}");
        }
    }

    public class Car
    {
        private ILog log;
        private Engine engine;

        public Car(Engine engine)
        {
            this.engine = engine;
            this.log = new EmailLog();
        }

        public Car(ILog log, Engine engine)
        {
            this.log = log;
            this.engine = engine;
        }

        public void Go()
        {
            engine.Ahead(200);
            log.Write("Car going forward...");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();

            //named paramter
            //builder.RegisterType<SMSLog>().As<ILog>()
            //    .WithParameter("phoneNumber", "+12345678");

            //typed parameter
            //builder.RegisterType<SMSLog>()
            //    .As<ILog>()
            //    .WithParameter(new TypedParameter(typeof(string), "+12345678"));

            //resolved parameter 
            //builder.RegisterType<SMSLog>()
            //    .As<ILog>()
            //    .WithParameter(
            //          new ResolvedParameter(
            //              // predicate
            //              (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "phoneNumber",
            //              // value accessor
            //              (pi, ctx) => "+12345678" // here I can use Component context to resolve something
            //              ));

            builder.Register((c, p) => new SMSLog(p.Named<string>("phoneNumber"))).As<ILog>();
            // Register SMSLog, specificing the constructor and a needed argument... 
            // ..Please do this at resolve time
            
            Console.WriteLine("About to build container...");
            var container = builder.Build();

            var random = new Random();
            var smsLog = container.Resolve<ILog>(new NamedParameter("phoneNumber", random.Next().ToString())); // Now, I register the SMSLog in such way that it requeries a parameter "phoneNUmber" of type string.
            smsLog.Write("test message");
        }
    }
}
