using Autofac;
using Autofac.Configuration;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Configuration
{
    public interface IOperation
    {
        float Calculate(int x, int y);
    }

    public class Addition : IOperation
    {
        public float Calculate(int x, int y)
        {
            return x + y;
        }
    }

    public class Multiplication : IOperation
    {
        public float Calculate(int x, int y)
        {
            return x * y;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // 1. Get configuration 
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("config.json");
            var configuration = configBuilder.Build();

            // 2. Use configuratio for AUTOFAC container
            var containerBuilder = new ContainerBuilder();
            var configModule = new ConfigurationModule(configuration);
            containerBuilder.RegisterModule(configModule);

        }
    }
}
