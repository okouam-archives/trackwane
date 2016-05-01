using System;
using paramore.brighter.commandprocessor;
using Trackwane.Framework.Common.Interfaces;

namespace Trackwane.Framework.Interfaces
{
    public interface IExecutionEngine : IAmACommandProcessor
    {
        event EventHandler<IRequest> MessagePosted;

        T Query<T>(string applicationKey) where T : IApplicationQuery;

        T Query<T>(string applicationKey, string organizationKey) where T : IOrganizationQuery;
    }
}