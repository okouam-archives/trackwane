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
    public class VehicleRegisteredListener : TransactionalListener<VehicleRegistered>
    {
        public VehicleRegisteredListener(IProvideTransactions transaction, IExecutionEngine publisher, ILog log) : base(transaction, publisher, log)
        {
        }

        protected override IEnumerable<DomainEvent> Handle(VehicleRegistered evt, IRepository repository)
        {
            var organization = repository.Load<Organization>(evt.OrganizationKey);

            if (organization == null)
            {
                throw new BusinessRuleException(PhraseBook.Generate(Message.UNKNOWN_ORGANIZATION, evt.OrganizationKey));
            }

            organization.Vehicles.Add(evt.Identifier);

            repository.Persist(organization);

            return Enumerable.Empty<DomainEvent>();
        }
    }
}
