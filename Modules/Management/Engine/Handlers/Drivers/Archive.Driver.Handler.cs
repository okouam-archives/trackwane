using System.Collections.Generic;
using log4net;
using Trackwane.Framework.Common;
using Trackwane.Framework.Common.Exceptions;
using Trackwane.Framework.Infrastructure.Requests;
using Trackwane.Framework.Interfaces;
using Trackwane.Management.Domain;
using Trackwane.Management.Engine.Commands.Drivers;
using Trackwane.Management.Engine.Services;

namespace Trackwane.Management.Engine.Handlers.Drivers
{
    public class ArchiveDriverHandler : TransactionalHandler<ArchiveDriver>
    {
        /* Public */

        public ArchiveDriverHandler(
            IProvideTransactions transaction,
            IExecutionEngine publisher,
            ILog log) :
            base(transaction, log)
        {
        }

        protected override IEnumerable<DomainEvent> Handle(ArchiveDriver cmd, IRepository repository)
        {
            var driver = repository.Find<Driver>(cmd.DriverId, cmd.ApplicationKey);

            if (driver == null)
            {
                throw new BusinessRuleException(PhraseBook.Generate(Message.UNKNOWN_DRIVER, cmd.DriverId));
            }

            driver.Archive();

            return driver.GetUncommittedChanges();
        }
    }
}
