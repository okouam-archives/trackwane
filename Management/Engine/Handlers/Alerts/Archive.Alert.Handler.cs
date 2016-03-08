using System.Collections.Generic;
using System.Linq;
using paramore.brighter.commandprocessor.Logging;
using Trackwane.Framework.Common;
using Trackwane.Framework.Common.Exceptions;
using Trackwane.Framework.Events;
using Trackwane.Framework.Infrastructure.Requests;
using Trackwane.Framework.Interfaces;
using Trackwane.Management.Commands.Alerts;
using Trackwane.Management.Domain;
using Trackwane.Management.Services;

namespace Trackwane.Management.Handlers.Alerts
{
    public class ArchiveAlertHandler : TransactionalHandler<ArchiveAlert>
    {
        public ArchiveAlertHandler(
            IProvideTransactions transaction,
            IExecutionEngine publisher,
            ILog log) :
            base(transaction, publisher, log)
        {
        }
        
        protected override IEnumerable<DomainEvent> Handle(ArchiveAlert cmd, IRepository repository)
        {
            var alert = repository.Load<Alert>(cmd.AlertId);

            if (alert == null)
            {
                throw new BusinessRuleException(PhraseBook.Generate(Message.UNKNOWN_ALERT, cmd.AlertId));
            }

            alert.Archive();

            return alert.GetUncommittedChanges().Cast<DomainEvent>();
        }
    }
}
