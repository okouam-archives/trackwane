using System;
using MassTransit;
using StructureMap;
using Trackwane.Framework.Common.Interfaces;
using Trackwane.Framework.Interfaces;

namespace Trackwane.Framework.Infrastructure
{
    public class ExecutionEngine : IExecutionEngine
    {
        private readonly IContainer container;
        private readonly IBusControl bus;

        public ExecutionEngine(IContainer container)
        {
            this.container = container;

            bus = Bus.Factory.CreateUsingInMemory(cfg =>
            {
                cfg.ReceiveEndpoint("queue_name", x =>
                {
                    x.LoadFrom(container);
                });
            });

        }

        public void Handle<T>(T cmd) where T : class
        {
            bus.Publish(cmd);
        }

        public T Query<T>(string applicationKey) where T : IApplicationQuery
        {
            var query = container.GetInstance<T>();
            query.ApplicationKey = applicationKey;
            return query;
        }

        public T Query<T>(string applicationKey, string organizationKey) where T : IOrganizationQuery
        {
            var query = container.GetInstance<T>();
            query.ApplicationKey = applicationKey;
            query.OrganizationKey = organizationKey;
            return query;
        }

        public void Start()
        {
            bus.Start();
        }
    }
}
