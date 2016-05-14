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
    public class GrantManagePermissionHandler : Handler<GrantManagePermission>
    {
        public GrantManagePermissionHandler(
            IProvideTransactions transaction,
            IExecutionEngine publisher, ILog log) : 
            base(publisher, transaction, log)
        {
        }

        protected override IEnumerable<DomainEvent> Handle(GrantManagePermission cmd, IRepository repository)
        {
            var organization = repository.Find<Organization>(cmd.OrganizationKey, cmd.ApplicationKey);

            if (organization == null)
            {
                throw new BusinessRuleException("Unable to find organization with key " + cmd.OrganizationKey);
            }

            var user = repository.Find<User>(cmd.UserKey, cmd.ApplicationKey);

            if (user == null)
            {
                throw new BusinessRuleException("Unable to find user with key " + cmd.UserKey);
            }

            organization.GrantManagePermission(user.Key);

            repository.Persist(organization);

            return organization.GetUncommittedChanges();
        }
    }
}
