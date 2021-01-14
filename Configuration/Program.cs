using Autofac;
using Autofac.Configuration;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;

namespace Configuration
{
    public interface IOperation
    {
        float Calculate(float x, float y);
    }

    public interface IOtherOperation
    {

    }

    public class Addition : IOperation, IOtherOperation
    {
        public float Calculate(float x, float y)
        {
            return x + y;
        }
    }

    public class Multiplication : IOperation, IOtherOperation
    {
        public float Calculate(float x, float y)
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

            // 3. Build container and components referenced in Json file
            using (var container = containerBuilder.Build())
            {
                float a = 3, b = 4;

                foreach (IOperation operation in container.Resolve<IList<IOperation>>())
                {
                    Console.WriteLine($"{operation.GetType().Name} of {a} and {b} = {operation.Calculate(a,b)}");
                }
            }
        }
    }
}
