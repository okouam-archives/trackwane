using System;
using Trackwane.Framework.Common.Interfaces;

namespace Trackwane.Framework.Interfaces
{
    public interface IExecutionEngine
    {
        void Handle<T>(T cmd) where T : class;

        T Query<T>(string applicationKey) where T : IApplicationQuery;

        T Query<T>(string applicationKey, string organizationKey) where T : IOrganizationQuery;

        void Start();
    }
}