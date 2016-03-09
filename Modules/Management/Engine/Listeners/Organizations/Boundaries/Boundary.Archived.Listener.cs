using System.Collections.Generic;
using System.Linq;
using paramore.brighter.commandprocessor.Logging;
using Trackwane.Framework.Common;
using Trackwane.Framework.Common.Exceptions;
using Trackwane.Framework.Infrastructure.Requests;
using Trackwane.Framework.Interfaces;
using Trackwane.Management.Domain;
using Trackwane.Management.Engine.Services;
using Trackwane.Management.Events;

namespace Trackwane.Management.Engine.Listeners.Organizations.Boundaries
{
    public class BoundaryArchivedListener : TransactionalListener<BoundaryArchived>
    {
        public BoundaryArchivedListener(IProvideTransactions transaction, IExecutionEngine publisher, ILog log) : base(transaction, publisher, log)
        {
        }

        protected override IEnumerable<DomainEvent> Handle(BoundaryArchived evt, IRepository repository)
        {
            var organization = repository.Load<Organization>(evt.OrganizationKey);

            var boundary = repository.Load<Boundary>(evt.BoundaryKey);

            if (boundary == null)
            {
                throw new BusinessRuleException(PhraseBook.Generate(Message.UNKNOWN_BOUNDARY, evt.BoundaryKey));
            }

            organization.Boundaries = organization.Boundaries.Where(x => x != boundary.Name).ToList();

            repository.Persist(organization);

            return Enumerable.Empty<DomainEvent>();
        }
    }
}
