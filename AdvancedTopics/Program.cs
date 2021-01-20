﻿using Autofac;
using Autofac.Core;
using Autofac.Core.Activators.Delegate;
using Autofac.Core.Registration;
using Autofac.Features.ResolveAnything;
using System;
using System.Collections.Generic;

namespace AdvancedTopics
{
    // Hard code that cannot be implemented inside the container
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

    public class HandlerRegistrationSource : IRegistrationSource
    {
        public bool IsAdapterForIndividualComponents => false;

        /// Invoke during the build stage. When the container is building up, will use all the registration sources... 
        public IEnumerable<IComponentRegistration> RegistrationsFor(Service service, Func<Service, IEnumerable<ServiceRegistration>> registrationAccessor)
        {
            // Detect the kind of registration the container is looking for and if match ours
            var swt = service as IServiceWithType;
            if(swt == null 
                || swt.ServiceType == null
                || !swt.ServiceType.IsAssignableTo<BaseHandler>())
            {
                yield break;
            }

            yield return new ComponentRegistration(
                Guid.NewGuid(),
                new DelegateActivator(
                    swt.ServiceType,
                    (context, parameters) =>
                    {
                        HandlerFactory provider = context.Resolve<HandlerFactory>();
                        // Get the method of the factory...GetHandler<T>()
                        var method = provider.GetType().GetMethod("GetHandler").MakeGenericMethod();
                        return method.Invoke(provider, null);
                    };
                    )

                );
            
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
