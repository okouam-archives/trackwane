using System.Collections.Generic;
using log4net;
using Trackwane.Framework.Common;
using Trackwane.Framework.Common.Exceptions;
using Trackwane.Framework.Infrastructure.Requests;
using Trackwane.Framework.Interfaces;
using Trackwane.Management.Domain;
using Trackwane.Management.Engine.Commands.Boundaries;
using Trackwane.Management.Engine.Services;

namespace Trackwane.Management.Engine.Handlers.Boundaries
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
            var boundary = repository.Find<Boundary>(cmd.BoundaryId, cmd.ApplicationKey);

            if (!string.IsNullOrEmpty(cmd.Name))
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
