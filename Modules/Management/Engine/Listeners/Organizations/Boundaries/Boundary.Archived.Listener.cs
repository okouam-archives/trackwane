using System.Collections.Generic;
using System.Linq;
using Trackwane.Framework.Common;
using Trackwane.Framework.Common.Exceptions;
using Trackwane.Framework.Infrastructure.Requests;
using Trackwane.Framework.Interfaces;
using Trackwane.Management.Contracts.Events;
using Trackwane.Management.Domain;
using Trackwane.Management.Engine.Services;
using log4net;

namespace Trackwane.Management.Engine.Listeners.Organizations.Boundaries
{
    public class BoundaryArchivedListener : TransactionalListener<BoundaryArchived>
    {
        public BoundaryArchivedListener(IProvideTransactions transaction, IExecutionEngine publisher, ILog log) : base(transaction, publisher, log)
        {
        }

        protected override IEnumerable<DomainEvent> Handle(BoundaryArchived evt, IRepository repository)
        {
            var organization = repository.Find<Organization>(evt.OrganizationKey, evt.ApplicationKey);

            var boundary = repository.Find<Boundary>(evt.BoundaryKey, evt.ApplicationKey);

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
