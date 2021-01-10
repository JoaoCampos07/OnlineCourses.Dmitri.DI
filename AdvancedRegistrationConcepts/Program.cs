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

     class Program
    {
        static void Main(string[] args)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(assembly)
                .Where(t => t.Name.EndsWith("Log")) // I want to register only types that have a name that ends with Log
                .Except<EmailLog>() // I dont want EmailLog to be register and be given for service ILog
                .Except<ConsoleLog>(c => c.As<ILog>().SingleInstance()) // I dont want to register ConsoleLog. I want it to be register this way...
                .AsSelf(); // All types are register was components without service

        }
    }
}

