using System.Collections.Generic;
using log4net;
using MassTransit;
using Trackwane.AccessControl.Contracts.Events;
using Trackwane.Framework.Common;
using Trackwane.Framework.Infrastructure.Requests;
using Trackwane.Framework.Interfaces;
using Trackwane.Management.Domain;

namespace Trackwane.Management.Engine.Listeners.Organizations
{
    public class OrganizationRegisteredListener : TransactionalListener<OrganizationRegistered>, IConsumer<OrganizationRegistered>
    {
        public OrganizationRegisteredListener(IProvideTransactions transaction, IExecutionEngine publisher, ILog log) : base(transaction, publisher, log)
        {
        }

        protected override IEnumerable<DomainEvent> Handle(OrganizationRegistered cmd, IRepository repository)
        {
            var organization = new Organization(cmd.ApplicationKey, cmd.OrganizationKey);
            repository.Persist(organization);
            return organization.GetUncommittedChanges();
        }
    }
}
