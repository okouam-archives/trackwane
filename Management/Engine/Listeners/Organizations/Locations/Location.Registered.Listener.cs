using System.Collections.Generic;
using System.Linq;
using paramore.brighter.commandprocessor.Logging;
using Trackwane.Framework.Common;
using Trackwane.Framework.Common.Exceptions;
using Trackwane.Framework.Events;
using Trackwane.Framework.Infrastructure.Requests;
using Trackwane.Framework.Interfaces;
using Trackwane.Management.Domain;
using Trackwane.Management.Events;
using Trackwane.Management.Services;

namespace Trackwane.Management.Listeners.Organizations.Locations
{
    public class LocationRegisteredListener : TransactionalListener<LocationRegistered>
    {
        public LocationRegisteredListener(IProvideTransactions transaction, IExecutionEngine publisher, ILog log) : base(transaction, publisher, log)
        {
        }

        protected override IEnumerable<DomainEvent> Handle(LocationRegistered evt, IRepository repository)
        {
            var location = repository.Load<Location>(evt.LocationKey);

            var organization = repository.Load<Organization>(location.OrganizationKey);

            if (organization == null)
            {
                throw new BusinessRuleException(PhraseBook.Generate(Message.UNKNOWN_ORGANIZATION, evt.OrganizationKey));
            }

            organization.Locations.Add(location.Name);

            repository.Persist(organization);

            return organization.GetUncommittedChanges();
        }
    }
}
