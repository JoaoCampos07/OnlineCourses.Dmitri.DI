using Autofac;
using Autofac.Core;
using Autofac.Core.Activators.Delegate;
using Autofac.Core.Lifetime;
using Autofac.Core.Registration;
using Autofac.Features.Metadata;
using Autofac.Features.ResolveAnything;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace AdvancedTopics
{
    public interface IReportingService
    {
        void Report();
    }

    public class ReportingService : IReportingService
    {
        public void Report()
        {
            Console.WriteLine("Here is your report");
        }
    }

    //Typical Decorator - reuses another service (that also implements IReportingService and DECORATES IT with reporting functionality)
    // Logger to log the initialize of reporting and the end of the reporting
    public class ReportingServiceWithLogging : IReportingService
    {
        private IReportingService _serviceToDecorate;

        public ReportingServiceWithLogging(IReportingService reportingService)
        {
            _serviceToDecorate = reportingService;
        }

        public void Report()
        {
            Console.WriteLine("Beginning of making the report...");
            this._serviceToDecorate.Report();
            Console.WriteLine("Report is concluded");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Can the Autofac provider the right components whem we implement the Decorator pattern ? 
            var b = new ContainerBuilder();
            b.RegisterType<ReportingService>().Named<IReportingService>("reporting");// mark by name
            b.RegisterDecorator<IReportingService>( // when asked for IReporting Service we will give ReportingServiceWithLogging with service to decorate!
             (context, service) => new ReportingServiceWithLogging(service),
             "reporting" // key to distingish service that we will be decorated, for other implementations of IReportingService
            );

            using (var c = b.Build())
            {
                c.Resolve<IReportingService>().Report();
            }
        }
    }
}
