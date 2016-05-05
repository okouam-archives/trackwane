using System;
using System.Collections.Generic;
using System.Linq;
using MassTransit;
using MassTransit.Internals.Extensions;
using StructureMap;
using Trackwane.Framework.Common.Interfaces;
using Trackwane.Framework.Interfaces;

namespace Trackwane.Framework.Infrastructure
{
    public class ExecutionEngine : IExecutionEngine
    {
        private readonly IContainer container;
        private IBusControl bus;

        public ExecutionEngine(IContainer container)
        {
            this.container = container;
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
            var types = FindTypes<IConsumer>(container, x => true);

            bus = Bus.Factory.CreateUsingInMemory(cfg =>
            {
                cfg.ReceiveEndpoint("queue_name", x =>
                {
                    x.LoadFrom(container);
                });
            });

            bus.Start();
        }

        static IList<Type> FindTypes<T>(IContainer container, Func<Type, bool> filter)
        {
            var types = container
                .Model
                .PluginTypes
                .Where(x => x.PluginType.HasInterface<T>())
                .Select(i => i.PluginType)
                .Concat(container.Model.InstancesOf<T>().Select(x => x.ReturnedType))
                .Where(filter)
                .Distinct()
                .ToList();
            return types;
        }
    }
}
