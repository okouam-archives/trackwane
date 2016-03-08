using System.Collections.Generic;
using System.Linq;
using paramore.brighter.commandprocessor.Logging;
using Trackwane.Framework.Common;
using Trackwane.Framework.Common.Exceptions;
using Trackwane.Framework.Events;
using Trackwane.Framework.Infrastructure.Requests;
using Trackwane.Framework.Interfaces;
using Trackwane.Management.Commands.Drivers;
using Trackwane.Management.Domain;
using Trackwane.Management.Services;

namespace Trackwane.Management.Handlers.Drivers
{
    public class ArchiveDriverHandler : TransactionalHandler<ArchiveDriver>
    {
        /* Public */

        public ArchiveDriverHandler(
            IProvideTransactions transaction,
            IExecutionEngine publisher,
            ILog log) :
            base(transaction, publisher, log)
        {
        }

        protected override IEnumerable<DomainEvent> Handle(ArchiveDriver cmd, IRepository repository)
        {
            var driver = repository.Load<Driver>(cmd.DriverId);

            if (driver == null)
            {
                throw new BusinessRuleException(PhraseBook.Generate(Message.UNKNOWN_DRIVER, cmd.DriverId));
            }

            driver.Archive();

            return driver.GetUncommittedChanges().Cast<DomainEvent>();
        }
    }
}
