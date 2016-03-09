using System.Configuration;
using System.Linq;
using Hangfire;
using Microsoft.Owin;
using Owin;
using Hangfire.StructureMap;
using log4net;
using log4net.Config;
using StructureMap;
using Trackwane.Simulator.Engine;
using Trackwane.Simulator.Engine.Commands;
using Trackwane.Simulator.Engine.Handlers;

[assembly: OwinStartup(typeof(Trackwane.Simulator.Web.Startup))]

namespace Trackwane.Simulator.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            XmlConfigurator.Configure();

            var container = ServiceLocator.Use();

            GlobalConfiguration
                .Configuration
                .UseStructureMapActivator(container)
                .UseSqlServerStorage("trackwane-simulator");
            
            app.UseHangfireDashboard();

            app.UseHangfireServer(new BackgroundJobServerOptions
            {
                WorkerCount = int.Parse(ConfigurationManager.AppSettings["WORKER_COUNT"])
            });

            var jobName = ConfigurationManager.AppSettings["JOB_NAME"];

            logger.Debug($"Configuring Hangfire by adding the <{jobName}> job");
            
            RecurringJob.RemoveIfExists(jobName);

            var frequency = ConfigurationManager.AppSettings["RETRIEVAL_FREQUENCY"];

            var buses = ConfigurationManager
             .AppSettings["BUSES"].Split(',')
             .Select(x => int.Parse(x.Trim()))
             .ToArray();

            var handler = container.GetInstance<SimulateSensorReadingsHandler>();

            RecurringJob.AddOrUpdate(jobName, () => RunJob(handler, buses), frequency);

            logger.Debug("Hangfire has been configured");
        }

        public static void RunJob(SimulateSensorReadingsHandler handler, int[] buses)
        {
            handler.Handle(new SimulateSensorReadings
            {
                Buses = buses
            });
        }

        private readonly ILog logger = LogManager.GetLogger(typeof (Startup));
    }
}