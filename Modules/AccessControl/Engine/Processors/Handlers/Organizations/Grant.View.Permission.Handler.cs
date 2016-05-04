using System.Collections.Generic;
using log4net;
using Trackwane.AccessControl.Domain.Organizations;
using Trackwane.AccessControl.Domain.Users;
using Trackwane.AccessControl.Engine.Commands.Organizations;
using Trackwane.Framework.Common;
using Trackwane.Framework.Common.Exceptions;
using Trackwane.Framework.Infrastructure.Requests;
using Trackwane.Framework.Interfaces;

namespace Trackwane.AccessControl.Engine.Processors.Handlers.Organizations
{
    public class GrantViewPermissionHandler : TransactionalHandler<GrantViewPermission>
    {
        public GrantViewPermissionHandler(
            IProvideTransactions transaction,
            IExecutionEngine publisher, ILog log) : 
            base(transaction, publisher, log)
        {
        }

        protected override IEnumerable<DomainEvent> Handle(GrantViewPermission cmd, IRepository repository)
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

            organization.GrantViewPermission(cmd.UserKey);

            repository.Persist(organization);

            return organization.GetUncommittedChanges();
        }
    }
}
