using Autofac;
using System;

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
        }
    }
}
