using System.Collections.Generic;
using paramore.brighter.commandprocessor.Logging;
using Trackwane.Framework.Common;
using Trackwane.Framework.Common.Exceptions;
using Trackwane.Framework.Infrastructure.Requests;
using Trackwane.Framework.Interfaces;
using Trackwane.Management.Commands.Boundaries;
using Trackwane.Management.Domain;
using Trackwane.Management.Services;

namespace Trackwane.Management.Handlers.Boundaries
{
    public class ArchiveBoundaryHandler : TransactionalHandler<ArchiveBoundary>
    {
        /* Public */

        public ArchiveBoundaryHandler(
            IProvideTransactions transaction,
            IExecutionEngine publisher,
            ILog log) :
            base(transaction, publisher, log)
        {
        }
        
        /* Protected */

        protected override IEnumerable<DomainEvent> Handle(ArchiveBoundary cmd, IRepository repository)
        {
            var boundary = repository.Load<Boundary>(cmd.BoundaryId);

            if (boundary == null)
            {
                throw new BusinessRuleException(PhraseBook.Generate(Message.UNKNOWN_BOUNDARY, cmd.BoundaryId));
            }

            boundary.Archive();

            repository.Persist(boundary);

            return boundary.GetUncommittedChanges();
        }
    }
}
