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
        private IReportingService _reportingService;

        public ReportingServiceWithLogging(IReportingService reportingService)
        {
            _reportingService = reportingService;
        }

        public void Report()
        {
            Console.WriteLine("Beginning of making the report...");
            this._reportingService.Report();
            Console.WriteLine("Report is concluded");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Can the Autofac provider the right components whem we implement the Decorator pattern ? 
            var b = new ContainerBuilder();
            b.RegisterType<ReportingServiceWithLogging>().As<IReportingService>();
            // Like it is above, it will end up in infinte loop or throught exception, because the reportingServiceWithLogging component
            // needs a IReportingService, so...Autofac understands that he needs a instance of itself. (ReportingServiceWIthLogging is as Service IReportingService)
            // And it sees that the instance that it needs (himself) needs also a IReportingService and so on, and so on, in a infinite loop.

            using (var c = b.Build())
            {
                c.Resolve<IReportingService>().Report();
            }
        }
    }
}
