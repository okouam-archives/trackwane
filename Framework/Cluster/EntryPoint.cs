using System;
using System.Diagnostics;
using System.Fabric.Query;
using System.Threading;
using Microsoft.ServiceFabric.Services.Runtime;
using StatelessService = Microsoft.ServiceFabric.Services.Runtime.StatelessService;

namespace Trackwane.Framework.Cluster
{
    public class EntryPoint<T> where T : StatelessService
    {
        public static void Launch(string serviceName)
        {
            try
            {
                ServiceRuntime.RegisterServiceAsync(serviceName, context => (StatelessService) Activator.CreateInstance(typeof(T), context)).GetAwaiter().GetResult();

                ServiceEventSource.Current.ServiceTypeRegistered(Process.GetCurrentProcess().Id, typeof(Node).Name);

                Thread.Sleep(Timeout.Infinite);
            }
            catch (Exception e)
            {
                ServiceEventSource.Current.ServiceHostInitializationFailed(e.ToString());
                throw;
            }
        }
    }
}
