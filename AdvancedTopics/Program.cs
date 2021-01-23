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

    //Decorator style
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
        }
    }
}
