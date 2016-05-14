using System.Collections.Generic;
using Trackwane.AccessControl.Domain.Organizations;
using Trackwane.AccessControl.Domain.Users;
using Trackwane.AccessControl.Engine.Commands.Organizations;
using Trackwane.Framework.Common;
using Trackwane.Framework.Common.Exceptions;
using Trackwane.Framework.Infrastructure.Requests;
using Trackwane.Framework.Interfaces;
using log4net;

namespace Trackwane.AccessControl.Engine.Processors.Handlers.Organizations
{
    public class RevokeReadAccessHandler : Handler<RevokeViewPermission>
    {
        public RevokeReadAccessHandler(IExecutionEngine engine, IProvideTransactions transaction, ILog log) : base(engine, transaction, log)
        {
        }

        protected override IEnumerable<DomainEvent> Handle(RevokeViewPermission cmd, IRepository repository)
        {
            var organization = repository.Find<Organization>(cmd.OrganizationKey, cmd.ApplicationKey);

            if (organization == null)
            {
                throw new BusinessRuleException();
            }

            var user = repository.Find<User>(cmd.UserKey, cmd.ApplicationKey);

            if (user == null)
            {
                throw new BusinessRuleException();
            }

            organization.RevokeViewPermission(user.Key);

            repository.Persist(organization);

            return organization.GetUncommittedChanges();
        }
    }
}
