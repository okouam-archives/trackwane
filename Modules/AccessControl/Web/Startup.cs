using System;
using Microsoft.Owin;
using Owin;
using Trackwane.AccessControl.Web;
using Trackwane.Framework.Infrastructure;
using Trackwane.Framework.Infrastructure.Factories;
using Trackwane.Framework.Infrastructure.Storage;

[assembly: OwinStartup(typeof(Startup))]
namespace Trackwane.AccessControl.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var serviceLocationFactory = new ServiceLocationFactory(new DocumentStoreBuilder(new DocumentStoreConfig()));

            var serviceLocator = new ServiceLocator<Engine.Registry>(serviceLocationFactory);

            var host = new EngineHost<Engine.Registry>(serviceLocator, new EngineHostConfig
            {
                ListenUri = new Uri("http://localhost:37483")
            });

            host.Start();
        }
    }
}
