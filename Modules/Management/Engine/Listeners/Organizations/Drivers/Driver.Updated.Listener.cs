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

namespace Trackwane.Management.Engine.Listeners.Organizations.Drivers
{
    public class DriverUpdatedListener : TransactionalListener<DriverUpdated>
    {
        public DriverUpdatedListener(IProvideTransactions transaction, IExecutionEngine publisher, ILog log) : base(transaction, publisher, log)
        {
        }

        protected override IEnumerable<DomainEvent> Handle(DriverUpdated evt, IRepository repository)
        {
            var organization = repository.Load<Organization>(evt.OrganizationKey);

            if (organization == null)
            {
                throw new BusinessRuleException(PhraseBook.Generate(Message.UNKNOWN_ORGANIZATION, evt.OrganizationKey));
            }

            organization.Drivers = organization.Drivers.Where(x => x != evt.Previous.Name).ToList();

            organization.Drivers.Add(evt.Current.Name);

            repository.Persist(organization);

            return Enumerable.Empty<DomainEvent>();
        }
    }
}
