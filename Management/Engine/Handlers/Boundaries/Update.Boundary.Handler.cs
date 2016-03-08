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
using Trackwane.Management.Services;

namespace Trackwane.Management.Handlers.Boundaries
{
    public class UpdateBoundaryHandler : TransactionalHandler<UpdateBoundary>
    {
        /* Public */

        public UpdateBoundaryHandler(
            IProvideTransactions transaction,
            IExecutionEngine publisher, ILog log) : 
            base(transaction, publisher, log)
        {
        }

        protected override IEnumerable<DomainEvent> Handle(UpdateBoundary cmd, IRepository repository)
        {
            var boundary = repository.Load<Boundary>(cmd.BoundaryId);

            if (!string.IsNullOrEmpty(cmd.Name))
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

                boundary.UpdateName(cmd.Name);
            }

            if (cmd.Coordinates != null)
            {
                boundary.UpdateCoordinates(cmd.Coordinates);
            }

            return boundary.GetUncommittedChanges();
        }
    }
}
