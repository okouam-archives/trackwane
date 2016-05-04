using System.Collections.Generic;
using System.Linq;
using log4net;
using Trackwane.Framework.Common;
using Trackwane.Framework.Common.Exceptions;
using Trackwane.Framework.Infrastructure.Requests;
using Trackwane.Framework.Interfaces;
using Trackwane.Management.Contracts.Events;
using Trackwane.Management.Domain;
using Trackwane.Management.Engine.Services;

namespace Trackwane.Management.Engine.Listeners.Organizations.Alerts
{
    public class AlertCreatedListener : TransactionalListener<AlertCreated>
    {
        public AlertCreatedListener(IProvideTransactions transaction, IExecutionEngine publisher, ILog log) : base(transaction, publisher, log)
        {
        }

        protected override IEnumerable<DomainEvent> Handle(AlertCreated evt, IRepository repository)
        {
            var organization = repository.Find<Organization>(evt.OrganizationKey, evt.ApplicationKey);

            if (organization == null)
            {
                throw new BusinessRuleException(PhraseBook.Generate(Message.UNKNOWN_ORGANIZATION, evt.OrganizationKey));
            }

            organization.Alerts.Add(evt.Name);

            repository.Persist(organization);

            return Enumerable.Empty<DomainEvent>();
        }
    }
}
