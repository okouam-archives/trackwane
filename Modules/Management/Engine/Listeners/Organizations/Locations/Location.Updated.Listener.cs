using System.Collections.Generic;
using System.Linq;
using log4net;
using MassTransit;
using Trackwane.Framework.Common;
using Trackwane.Framework.Common.Exceptions;
using Trackwane.Framework.Infrastructure.Requests;
using Trackwane.Framework.Interfaces;
using Trackwane.Management.Contracts.Events;
using Trackwane.Management.Domain;
using Message = Trackwane.Management.Engine.Services.Message;

namespace Trackwane.Management.Engine.Listeners.Organizations.Locations
{
    public class LocationUpdatedListener : TransactionalListener<LocationUpdated>, IConsumer<LocationUpdated>
    {
        public LocationUpdatedListener(IProvideTransactions transaction, IExecutionEngine publisher, ILog log) : base(transaction, publisher, log)
        {
        }

        protected override IEnumerable<DomainEvent> Handle(LocationUpdated evt, IRepository repository)
        {
            var organization = repository.Find<Organization>(evt.OrganizationKey, evt.ApplicationKey);

            if (organization == null)
            {
                throw new BusinessRuleException(PhraseBook.Generate(Message.UNKNOWN_ORGANIZATION, evt.OrganizationKey));
            }

            organization.Locations = organization.Locations.Where(x => x != evt.Previous.Name).ToList();

            organization.Locations.Add(evt.Current.Name);

            repository.Persist(organization);

            return Enumerable.Empty<DomainEvent>();
        }
    }
}
