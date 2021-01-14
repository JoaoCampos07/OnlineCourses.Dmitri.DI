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
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder); 
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
