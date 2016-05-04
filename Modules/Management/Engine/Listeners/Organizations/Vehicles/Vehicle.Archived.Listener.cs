using System.Collections.Generic;
using System.Linq;
using log4net;
using Trackwane.Framework.Common;
using Trackwane.Framework.Common.Exceptions;
using Trackwane.Framework.Infrastructure.Requests;
using Trackwane.Framework.Interfaces;
using Trackwane.Management.Contracts.Events;
using Trackwane.Management.Domain;
using Message = Trackwane.Management.Engine.Services.Message;

namespace Trackwane.Management.Engine.Listeners.Organizations.Vehicles
{
    public class VehicleArchivedListener : TransactionalListener<VehicleArchived>
    {
        public VehicleArchivedListener(IProvideTransactions transaction, IExecutionEngine publisher, ILog log) : base(transaction, publisher, log)
        {
        }

        protected override IEnumerable<DomainEvent> Handle(VehicleArchived evt, IRepository repository)
        {
            var organization = repository.Find<Organization>(evt.OrganizationKey, evt.ApplicationKey);

            if (organization == null)
            {
                throw new BusinessRuleException(PhraseBook.Generate(Message.UNKNOWN_ORGANIZATION, evt.OrganizationKey));
            }

            var boundary = repository.Find<Vehicle>(evt.VehicleKey, evt.ApplicationKey);

            organization.Vehicles = organization.Vehicles.Where(x => x != boundary.Identifier).ToList();

            repository.Persist(organization);

            return Enumerable.Empty<DomainEvent>();
        }
    }
}
