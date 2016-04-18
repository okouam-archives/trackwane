using System.Collections.Generic;
using paramore.brighter.commandprocessor.Logging;
using Trackwane.Framework.Common;
using Trackwane.Framework.Common.Exceptions;
using Trackwane.Framework.Infrastructure.Requests;
using Trackwane.Framework.Interfaces;
using Trackwane.Management.Domain;
using Trackwane.Management.Engine.Commands.Boundaries;
using Message = Trackwane.Management.Engine.Services.Message;

namespace Trackwane.Management.Engine.Handlers.Boundaries
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
            var organization = repository.Find<Organization>(cmd.OrganizationId, cmd.ApplicationKey);

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
                var existingBoundary = repository.Find<Boundary>(cmd.BoundaryId, cmd.ApplicationKey);

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
