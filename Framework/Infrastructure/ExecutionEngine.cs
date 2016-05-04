using System;
using StructureMap;
using Trackwane.Framework.Common.Interfaces;
using Trackwane.Framework.Interfaces;

namespace Trackwane.Framework.Infrastructure
{
    public class ExecutionEngine : IExecutionEngine
    {
        private readonly IContainer container;

        public ExecutionEngine(IContainer container)
        {
            this.container = container;
        }

        public void Handle<T>(T cmd)
        {
            throw new NotImplementedException();
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
    }
}
