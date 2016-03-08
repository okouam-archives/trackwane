using System.Collections.Generic;
using System.Linq;
using paramore.brighter.commandprocessor.Logging;
using Trackwane.AccessControl.Events;
using Trackwane.Framework.Common;
using Trackwane.Framework.Events;
using Trackwane.Framework.Infrastructure.Requests;
using Trackwane.Framework.Interfaces;
using Trackwane.Management.Domain;

namespace Trackwane.Management.Listeners.Organizations
{
    public class OrganizationRegisteredListener : TransactionalListener<OrganizationRegistered>
    {
        public OrganizationRegisteredListener(IProvideTransactions transaction, IExecutionEngine publisher, ILog log) : base(transaction, publisher, log)
        {
        }

        protected override IEnumerable<DomainEvent> Handle(OrganizationRegistered cmd, IRepository repository)
        {
            var organization = new Organization(cmd.OrganizationKey);
            repository.Persist(organization);
            return organization.GetUncommittedChanges();
        }
    }
}
