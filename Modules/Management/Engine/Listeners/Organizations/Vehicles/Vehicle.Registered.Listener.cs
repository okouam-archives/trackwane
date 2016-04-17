using System.Collections.Generic;
using System.Linq;
using paramore.brighter.commandprocessor.Logging;
using Trackwane.Framework.Common;
using Trackwane.Framework.Common.Exceptions;
using Trackwane.Framework.Infrastructure.Requests;
using Trackwane.Framework.Interfaces;
using Trackwane.Management.Contracts.Events;
using Trackwane.Management.Domain;
using Message = Trackwane.Management.Engine.Services.Message;

namespace Trackwane.Management.Engine.Listeners.Organizations.Vehicles
{
    public class VehicleRegisteredListener : TransactionalListener<VehicleRegistered>
    {
        public VehicleRegisteredListener(IProvideTransactions transaction, IExecutionEngine publisher, ILog log) : base(transaction, publisher, log)
        {
        }

        protected override IEnumerable<DomainEvent> Handle(VehicleRegistered evt, IRepository repository)
        {
            var organization = repository.Find<Organization>(evt.OrganizationKey, evt.ApplicationKey);

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
