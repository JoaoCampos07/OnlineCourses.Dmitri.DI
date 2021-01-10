using Autofac;
using Autofac.Core;
using System;
using System.Reflection;

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

    public class Parent
    {
        public override string ToString()
        => "I'm your father";
    }

    public class Child
    {
        public string Name { get; set; }
        public Parent Parent { get; set; }

        public void SetParent(Parent parent)
            => Parent = parent;
    }

    // Module responsbile for registering the class's : Child and Parent
    public class ParentChildModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Parent>();
            builder.Register(c => new Child() { Parent = c.Resolve<Parent>() });
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyModules(typeof(Program).Assembly); // 1 Way
            builder.RegisterAssemblyModules<ParentChildModule>(typeof(Program).Assembly); // 2 Way

            var container = builder.Build();
            Console.WriteLine(container.Resolve<Child>().Parent);
        }
    }
}

