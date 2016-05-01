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
            var alert = repository.Find<Alert>(cmd.AlertId, cmd.ApplicationKey);

            if (alert == null)
            {
                throw new BusinessRuleException(PhraseBook.Generate(Message.UNKNOWN_ALERT, cmd.AlertId));
            }

            alert.Archive();

            return alert.GetUncommittedChanges();
        }
    }
}
