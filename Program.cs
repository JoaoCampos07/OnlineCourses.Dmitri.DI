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
            builder.RegisterType<ConsoleLog>().As<ILog>().AsSelf();
            builder.RegisterType<Engine>(); // If I take this out, AUTOFAC cannot resolve the parameter engine of type Engine
            builder.RegisterType<Car>();

            IContainer container = builder.Build();

            var car = container.Resolve<Car>(); // Recursive process...AutoFac will try to solve the car, it sees that it
            // needs a engine, them it sees that the engine needs a ILog, go get it, resolve the engine, and finally resolve the car.
            car.Go();
        }
    }
}
