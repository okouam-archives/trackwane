using System.Collections.Generic;
using System.Linq;
using paramore.brighter.commandprocessor.Logging;
using Trackwane.Framework.Common;
using Trackwane.Framework.Common.Exceptions;
using Trackwane.Framework.Infrastructure.Requests;
using Trackwane.Framework.Interfaces;
using Trackwane.Management.Contracts.Events;
using Trackwane.Management.Domain;
using Trackwane.Management.Engine.Services;

namespace Trackwane.Management.Engine.Listeners.Organizations.Alerts
{
    public class AlertArchivedListener : TransactionalListener<AlertArchived>
    {
        public AlertArchivedListener(IProvideTransactions transaction, IExecutionEngine publisher, ILog log) : base(transaction, publisher, log)
        {
        }

        protected override IEnumerable<DomainEvent> Handle(AlertArchived evt, IRepository repository)
        {
            var organization = repository.Load<Organization>(evt.OrganizationKey);

            if (organization == null)
            {
                throw new BusinessRuleException(PhraseBook.Generate(Message.UNKNOWN_ORGANIZATION, evt.OrganizationKey));
            }
            
            var alert = repository.Load<Alert>(evt.AlertKey);

            if (alert == null)
            {
                throw new BusinessRuleException(PhraseBook.Generate(Message.UNKNOWN_ALERT, evt.AlertKey));
            }

            organization.Alerts = organization.Alerts.Where(x => x != alert.Name).ToList();

            repository.Persist(organization);

            return Enumerable.Empty<DomainEvent>();
        }
    }
}
