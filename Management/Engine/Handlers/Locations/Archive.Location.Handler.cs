using System.Collections.Generic;
using System.Linq;
using paramore.brighter.commandprocessor.Logging;
using Trackwane.Framework.Common;
using Trackwane.Framework.Common.Exceptions;
using Trackwane.Framework.Events;
using Trackwane.Framework.Infrastructure.Requests;
using Trackwane.Framework.Interfaces;
using Trackwane.Management.Commands.Locations;
using Trackwane.Management.Domain;
using Trackwane.Management.Services;

namespace Trackwane.Management.Handlers.Locations
{
    public class ArchiveLocationHandler : TransactionalHandler<ArchiveLocation>
    {
        public ArchiveLocationHandler(
            IProvideTransactions transaction,
            IExecutionEngine publisher,
            ILog log) :
            base(transaction, publisher, log)
        {
        }

        protected override IEnumerable<DomainEvent> Handle(ArchiveLocation cmd, IRepository repository)
        {
            var location = repository.Load<Location>(cmd.LocationId);

            if (location == null)
            {
                throw new BusinessRuleException(PhraseBook.Generate(Message.UNKNOWN_LOCATION, cmd.LocationId));
            }

            location.Archive();

            repository.Persist(location);

            return location.GetUncommittedChanges().Cast<DomainEvent>();
        }
    }
}
