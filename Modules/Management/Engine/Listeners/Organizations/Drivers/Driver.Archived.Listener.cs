using System.Collections.Generic;
using System.Linq;
using paramore.brighter.commandprocessor.Logging;
using Trackwane.Framework.Common;
using Trackwane.Framework.Common.Exceptions;
using Trackwane.Framework.Infrastructure.Requests;
using Trackwane.Framework.Interfaces;
using Trackwane.Management.Domain;
using Trackwane.Management.Engine.Services;
using Trackwane.Management.Events;

namespace Trackwane.Management.Engine.Listeners.Organizations.Drivers
{
    public class DriverArchivedListener : TransactionalListener<DriverArchived>
    {
        public DriverArchivedListener(IProvideTransactions transaction, IExecutionEngine publisher, ILog log) : base(transaction, publisher, log)
        {
        }

        protected override IEnumerable<DomainEvent> Handle(DriverArchived evt, IRepository repository)
        {
            var driver = repository.Load<Driver>(evt.DriverKey);

            if (driver == null)
            {
                throw new BusinessRuleException(PhraseBook.Generate(Message.UNKNOWN_DRIVER, evt.DriverKey));
            }

            var organization = repository.Load<Organization>(driver.OrganizationKey);

            if (organization == null)
            {
                throw new BusinessRuleException(PhraseBook.Generate(Message.UNKNOWN_ORGANIZATION, evt.OrganizationKey));
            }

            if (organization.Drivers.Any())
            {
                organization.Drivers = organization.Drivers.Where(x => x != driver.Name).ToList();

                return organization.GetUncommittedChanges();
            }

            return Enumerable.Empty<DomainEvent>();
        }
    }
}
