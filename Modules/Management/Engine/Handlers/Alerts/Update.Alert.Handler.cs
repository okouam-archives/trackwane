using System.Collections.Generic;
using paramore.brighter.commandprocessor.Logging;
using Trackwane.Framework.Common;
using Trackwane.Framework.Common.Exceptions;
using Trackwane.Framework.Infrastructure.Requests;
using Trackwane.Framework.Interfaces;
using Trackwane.Management.Domain;
using Trackwane.Management.Engine.Commands.Alerts;
using Trackwane.Management.Engine.Services;

namespace Trackwane.Management.Engine.Handlers.Alerts
{
    public class UpdateAlertHandler : TransactionalHandler<UpdateAlert>
    {
        public UpdateAlertHandler(IProvideTransactions transaction, IExecutionEngine publisher, ILog log) : base(transaction, publisher, log)
        {
        }
        
        protected override IEnumerable<DomainEvent> Handle(UpdateAlert cmd, IRepository repository)
        {
            var alert = repository.Find<Alert>(cmd.AlertId, cmd.ApplicationKey);

            if (alert == null)
            {
                throw new BusinessRuleException(PhraseBook.Generate(Message.UNKNOWN_ALERT, cmd.AlertId));
            }

            if (cmd.Name != null)
            {
                var organization = repository.Find<Organization>(cmd.OrganizationId, cmd.ApplicationKey);

                if (organization == null)
                {
                    throw new BusinessRuleException(PhraseBook.Generate(Message.UNKNOWN_ORGANIZATION, cmd.OrganizationId));
                }

                if (organization.Alerts.Contains(cmd.Name))
                {
                    throw new BusinessRuleException(PhraseBook.Generate(Message.DUPLICATE_ALERT_NAME, cmd.Name));
                }
            }

            alert.Edit(cmd.Name);

            return alert.GetUncommittedChanges();
        }
    }
}
