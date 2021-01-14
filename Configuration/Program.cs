using Autofac;
using System;

namespace Configuration
{
    public interface IDriver
    {
        void Drive();
    }

    public interface IVehicle
    {
        void Go();
    }

    public class Truck : IVehicle
    {
        private readonly IDriver driver;

        public Truck(IDriver driver)
        {
            this.driver = driver;
        }

        public void Go()
        {
            this.driver.Drive();
        }
    }

    public class SaneDriver : IDriver
    {
        public void Drive()
        {
            Console.WriteLine("Drive safely into the destination.");
        }
    }

    public class CrazyDriver : IDriver
    {
        public void Drive()
        {
            Console.WriteLine("Driving too fast and crashing into a tree.");
        }
    }

    public class TransportModule : Module
    {
        // When i consctruct the module i can control this flag...
        public bool ObeySpeedLimit { get; set; }

        protected override void Load(ContainerBuilder builder)
        {
            if (ObeySpeedLimit)
                builder.RegisterType<SaneDriver>().As<IDriver>();
            else
                builder.RegisterType<CrazyDriver>().As<IDriver>();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var x = new ContainerBuilder();
            x.RegisterModule(new TransportModule { ObeySpeedLimit = false });
            using (var container = x.Build())
            {
                container.Resolve<IDriver>().Drive();
            }
        }
    }
}
