using System;
using Hangfire;
using HangFire.Raven.Storage;
using Microsoft.Owin;
using Microsoft.Owin.Diagnostics;
using Owin;
using Trackwane.Data.Client;
using Trackwane.Framework.Common.Configuration;
using Trackwane.Simulator.Engine.Commands;
using Trackwane.Simulator.Engine.Handlers;
using Trackwane.Simulator.Engine.Queries;
using Trackwane.Simulator.Engine.Services;
using Trackwane.Simulator.Standalone;

[assembly: OwinStartup(typeof(Startup))]

namespace Trackwane.Simulator.Standalone
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseErrorPage(new ErrorPageOptions
            {
                ShowCookies = true,
                ShowSourceCode = true,
                ShowHeaders = true, 
                ShowExceptionDetails = true,
                SourceCodeLineCount = 10,
                ShowEnvironment = true,
                ShowQuery = true
            });

            app.UseWelcomePage("/");

            var documentStoreConfig = new DocumentStoreConfig();

            if (documentStoreConfig.UseEmbedded)
            {
                GlobalConfiguration.Configuration.UseEmbeddedRavenStorage();
            }
            else
            {
                GlobalConfiguration.Configuration.UseRavenStorage(documentStoreConfig.Url, documentStoreConfig.Name, documentStoreConfig.ApiKey);
            }
            
            app.UseHangfireDashboard();
            app.UseHangfireServer();

            RecurringJob.AddOrUpdate("sdfsdf", () => RunJob(), Cron.Minutely);
        }

        public static void RunJob()
        {
            var positionProvider = new ReadingProvider(new Config());
            var dataContext = new DataContext("http://localhost:37470", new PlatformConfig()).UseWithoutAuthentication();
            var handler = new SimulateSensorReadingsHandler(dataContext, new GetVehicleReadings(positionProvider, new FindVehicles(positionProvider, new GetRoutes(new Config()))));

            handler.Handle(new SimulateSensorReadings
            {
                Buses = new[] { 1004, 1006, 1009 }
            });
        }
    }
}