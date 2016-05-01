using System.Collections.Generic;
using System.Linq;
using paramore.brighter.commandprocessor.Logging;
using Trackwane.Framework.Common;
using Trackwane.Framework.Common.Exceptions;
using Trackwane.Framework.Infrastructure.Requests;
using Trackwane.Framework.Interfaces;
using Trackwane.Management.Contracts.Events;
using Trackwane.Management.Domain;
using Trackwane.Management.Engine.Services;

namespace Trackwane.Management.Engine.Listeners.Organizations.Locations
{
    public class LocationArchivedListener : TransactionalListener<LocationArchived>
    {
        public LocationArchivedListener(IProvideTransactions transaction, IExecutionEngine publisher, ILog log) : base(transaction, publisher, log)
        {
        }

        protected override IEnumerable<DomainEvent> Handle(LocationArchived evt, IRepository repository)
        {
            var location = repository.Find<Location>(evt.LocationKey, evt.ApplicationKey);

            var organization = repository.Find<Organization>(location.OrganizationKey, evt.ApplicationKey);

            if (organization == null)
            {
                throw new BusinessRuleException(PhraseBook.Generate(Message.UNKNOWN_ORGANIZATION, evt.OrganizationKey));
            }

            organization.Locations = organization.Locations.Where(x => x != location.Name).ToList();

            repository.Persist(organization);

            return organization.GetUncommittedChanges();
        }
    }
}
