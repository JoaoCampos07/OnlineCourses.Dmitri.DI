using Autofac;
using Autofac.Features.ResolveAnything;
using System;

namespace AdvancedTopics
{
    // Defines way of generating event handler
    public abstract class BaseHandler
    {
        public virtual string Handle(string msg)
        {
            return "Handled: " + msg;
        }
    }

    public class HandlerA : BaseHandler
    {
        public override string Handle(string msg)
        {
            return "Handled by A: " + msg;
        }
    }

    public class HandlerB : BaseHandler
    {
        public override string Handle(string msg)
        {
            return "Handled by B: " + msg;
        }
    }

    public interface IHandlerFactory
    {
        T GetHandler<T>() where T : BaseHandler;
    }

    public class HandlerFactory : IHandlerFactory
    {
        public T GetHandler<T>() where T : BaseHandler
        {
            return Activator.CreateInstance<T>(); // this method does not belong to AutoFac. And is generic to create some obj that is type BaseHandler
        }
    }

    // Consumer class's that need Handler objs 
    public class ConsumerA
    {
        private readonly HandlerA handlerA;

        public ConsumerA(HandlerA handlerA)
        {
            this.handlerA = handlerA;
        }

        public void DoWork()
        {
            Console.WriteLine(handlerA.Handle("Consumer A"));
        }
    }

    public class ConsumerB
    {
        private readonly HandlerA handlerA;

        public ConsumerB(HandlerA handlerA)
        {
            this.handlerA = handlerA;
        }

        public void DoWork()
        {
            Console.WriteLine(handlerA.Handle("Consumer B"));
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var b = new ContainerBuilder();
        }
    }
}
