using Autofac;
using System;

namespace OnlineCourses.Dmitri.DI
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
            builder.RegisterType<ConsoleLog>().As<ILog>();

            // Way 1 : 
            //var engine = new Engine(new ConsoleLog(), 123); let's express this by 
            //builder.RegisterInstance(engine);

            // Way 2 :
            builder.Register((IComponentContext c) => new Engine(c.Resolve<ILog>(), 123)); // We will provide always the same engine
            builder.RegisterType<Car>()
                .UsingConstructor(typeof(Engine));

            IContainer container = builder.Build();

            var car = container.Resolve<Car>(); 
            car.Go();
        }
    }
}

// QUESTIONS : How Resolve works with two constructors ? 
