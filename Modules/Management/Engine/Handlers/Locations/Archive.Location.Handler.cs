using System.Collections.Generic;
using paramore.brighter.commandprocessor.Logging;
using Trackwane.Framework.Common;
using Trackwane.Framework.Common.Exceptions;
using Trackwane.Framework.Infrastructure.Requests;
using Trackwane.Framework.Interfaces;
using Trackwane.Management.Domain;
using Trackwane.Management.Engine.Commands.Locations;
using Trackwane.Management.Engine.Services;

namespace Trackwane.Management.Engine.Handlers.Locations
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
            var location = repository.Find<Location>(cmd.LocationId, cmd.ApplicationKey);

            if (location == null)
            {
                throw new BusinessRuleException(PhraseBook.Generate(Message.UNKNOWN_LOCATION, cmd.LocationId));
            }

            location.Archive();

            repository.Persist(location);

            return location.GetUncommittedChanges();
        }
    }
}
