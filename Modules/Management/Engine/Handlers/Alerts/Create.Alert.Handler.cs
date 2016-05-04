using System.Collections.Generic;
using log4net;
using Trackwane.Framework.Common;
using Trackwane.Framework.Common.Exceptions;
using Trackwane.Framework.Infrastructure.Requests;
using Trackwane.Framework.Interfaces;
using Trackwane.Management.Domain;
using Trackwane.Management.Engine.Commands.Alerts;
using Trackwane.Management.Engine.Services;

namespace Trackwane.Management.Engine.Handlers.Alerts
{
    public class CreateAlertHandler : TransactionalHandler<CreateAlert>
    {
        public CreateAlertHandler(IProvideTransactions transaction,
            IExecutionEngine publisher, 
            ILog log) :
            base(transaction, publisher, log)
        {
        }

        protected override IEnumerable<DomainEvent> Handle(CreateAlert cmd, IRepository repository)
        {
            var organization = repository.Find<Organization>(cmd.OrganizationKey, cmd.ApplicationKey);

            if (organization == null)
            {
                throw new BusinessRuleException(PhraseBook.Generate(Message.UNKNOWN_ORGANIZATION, cmd.OrganizationKey));
            }

            if (organization.Alerts.Contains(cmd.Name))
            {
                throw new BusinessRuleException(PhraseBook.Generate(Message.DUPLICATE_ALERT_NAME, cmd.Name));
            }

            var alert = new Alert(cmd.ApplicationKey, cmd.AlertKey, cmd.Name, cmd.OrganizationKey);

            repository.Persist(alert);

            return alert.GetUncommittedChanges();
        }
    }
}
