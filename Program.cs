using Autofac;
using System;

namespace OnlineCourses.Dmitri.DI
{
    public interface ILog
    {
        void Write(string message); 
    }

    public interface IConsoleLog
    {

    }

    public class ConsoleLog : ILog 
    {
        public void Write(string message)
        {
            Console.WriteLine(message);
        }
    }

    public class EmailLog : ILog, IConsoleLog
    {
        private const string adminEmail = "admin@foo.com";
        public void Write(string message)
        {
            Console.WriteLine($"Email sent to : {adminEmail} : {message}");
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

        public void Ahead(int power)
        {
            log.Write($"Engine [{id}] ahead {power}");
        }
    }

    public class Car
    {
        private ILog log;
        private Engine engine;

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
            //register components before build
            builder.RegisterType<EmailLog>()
                .As<ILog>()
                .As<IConsoleLog>();
            builder.RegisterType<ConsoleLog>().As<ILog>().AsSelf().PreserveExistingDefaults();
            builder.RegisterType<Engine>(); 
            builder.RegisterType<Car>();

            IContainer container = builder.Build();

            var log = container.Resolve<ILog>();
            var log2 = container.Resolve<IConsoleLog>();

            var car = container.Resolve<Car>(); 
            car.Go();
        }
    }
}
