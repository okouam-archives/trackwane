using System.Collections.Generic;
using System.Linq;
using paramore.brighter.commandprocessor.Logging;
using Trackwane.Framework.Common;
using Trackwane.Framework.Common.Exceptions;
using Trackwane.Framework.Events;
using Trackwane.Framework.Infrastructure.Requests;
using Trackwane.Framework.Interfaces;
using Trackwane.Management.Commands.Boundaries;
using Trackwane.Management.Domain;
using Message = Trackwane.Management.Services.Message;

namespace Trackwane.Management.Handlers.Boundaries
{
    public class CreateBoundaryHandler : TransactionalHandler<CreateBoundary>
    {

        public CreateBoundaryHandler(
            IProvideTransactions transaction,
            IExecutionEngine publisher, 
            ILog log) :
            base(transaction, publisher, log)
        {
        }

        /* Protected */

        protected override IEnumerable<DomainEvent> Handle(CreateBoundary cmd, IRepository repository)
        {
            var organization = repository.Load<Organization>(cmd.OrganizationId);

            if (organization == null)
            {
                throw new BusinessRuleException(PhraseBook.Generate(Message.UNKNOWN_ORGANIZATION, cmd.OrganizationId));
            }

            if (organization.Boundaries.Contains(cmd.Name))
            {
                throw new BusinessRuleException(PhraseBook.Generate(Message.DUPLICATE_BOUNDARY_NAME, cmd.Name));
            }

            if (!string.IsNullOrEmpty(cmd.BoundaryId))
            {
                var existingBoundary = repository.Load<Boundary>(cmd.BoundaryId);

                if (existingBoundary != null)
                {
                    throw new BusinessRuleException(PhraseBook.Generate(Message.UNKNOWN_BOUNDARY, cmd.BoundaryId));
                }
            }

            var boundary = new Boundary(cmd.BoundaryId, cmd.OrganizationId, cmd.Name, cmd.Coordinates);

            repository.Persist(boundary);

            return boundary.GetUncommittedChanges();
        }
    }
}
