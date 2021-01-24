using Autofac;
using Autofac.Core;
using Autofac.Core.Activators.Delegate;
using Autofac.Core.Lifetime;
using Autofac.Core.Registration;
using Autofac.Extras.AggregateService;
using Autofac.Extras.AttributeMetadata;
using Autofac.Extras.DynamicProxy;
using Autofac.Features.AttributeFilters;
using Autofac.Features.Metadata;
using Autofac.Features.ResolveAnything;
using Castle.DynamicProxy;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;

namespace AdvancedTopics
{
    // We want attach login functiontality to any method that is called inside some type(specific obj)

    //way of intercepting the calling of any method that belong to a particular type:
    public class CallLogger : IInterceptor
    {
        private TextWriter _textWriter;

        public CallLogger(TextWriter textWriter)
        {
            _textWriter = textWriter;
        }

        // extra actions when something is called 
        public void Intercept(IInvocation invocation)
        {
            // intercept method call
            var methodName = invocation.Method.Name;
            _textWriter.WriteLine("Calling method {0} with args {1}",
                methodName,
                string.Join(",",
                invocation.Arguments.Select(arg => (arg ?? string.Empty).ToString())
                ));
            // Invocation it self...invocation continues
            invocation.Proceed();

            _textWriter.WriteLine("Done calling method {0}, result was {1}",
                methodName,
                invocation.ReturnValue);
                
        }
    }

    public interface IAudit
    {
        int Start(DateTime reportData);
    }

    // Whever somebody calls a member of Audit, which is a method, please intercept that call and log it into File system.
    [Intercept(typeof(CallLogger))]
    public class Audit : IAudit
    {
        public int Start(DateTime reportData)
        {
            Console.WriteLine($"Starting the report on {reportData}");
            return 42;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {

        }
    }
}
