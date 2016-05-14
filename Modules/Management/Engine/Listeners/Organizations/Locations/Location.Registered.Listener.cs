using System.Collections.Generic;
using log4net;
using MassTransit;
using Trackwane.Framework.Common;
using Trackwane.Framework.Common.Exceptions;
using Trackwane.Framework.Infrastructure.Requests;
using Trackwane.Framework.Interfaces;
using Trackwane.Management.Contracts.Events;
using Trackwane.Management.Domain;
using Trackwane.Management.Engine.Services;

namespace Trackwane.Management.Engine.Listeners.Organizations.Locations
{
    public class LocationRegisteredListener : TransactionalListener<LocationRegistered>, IConsumer<LocationRegistered>
    {
        public LocationRegisteredListener(IProvideTransactions transaction, IExecutionEngine publisher, ILog log) : base(transaction, publisher, log)
        {
        }

        protected override IEnumerable<DomainEvent> Handle(LocationRegistered evt, IRepository repository)
        {
            var location = repository.Find<Location>(evt.LocationKey, evt.ApplicationKey);

            var organization = repository.Find<Organization>(location.OrganizationKey, evt.ApplicationKey);

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
