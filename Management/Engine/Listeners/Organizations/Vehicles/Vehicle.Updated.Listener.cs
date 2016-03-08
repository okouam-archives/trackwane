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
using Message = Trackwane.Management.Services.Message;

namespace Trackwane.Management.Listeners.Organizations.Vehicles
{
    public class VehicleUpdatedListener : TransactionalListener<VehicleUpdated>
    {
        public VehicleUpdatedListener(IProvideTransactions transaction, IExecutionEngine publisher, ILog log) : base(transaction, publisher, log)
        {
        }

        protected override IEnumerable<DomainEvent> Handle(VehicleUpdated evt, IRepository repository)
        {
            var organization = repository.Load<Organization>(evt.OrganizationKey);

            if (organization == null)
            {
                throw new BusinessRuleException(PhraseBook.Generate(Message.UNKNOWN_ORGANIZATION, evt.OrganizationKey));
            }

            organization.Vehicles = organization.Vehicles.Where(x => x != evt.Previous.Identifier).ToList();

            organization.Vehicles.Add(evt.Current.Identifier);

            repository.Persist(organization);

            return Enumerable.Empty<DomainEvent>();
        }
    }
}
