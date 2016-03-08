using System.Collections.Generic;
using System.Linq;
using paramore.brighter.commandprocessor.Logging;
using Trackwane.Framework.Common;
using Trackwane.Framework.Common.Exceptions;
using Trackwane.Framework.Events;
using Trackwane.Framework.Infrastructure.Requests;
using Trackwane.Framework.Interfaces;
using Trackwane.Management.Commands.Locations;
using Trackwane.Management.Domain;

namespace Trackwane.Management.Handlers.Locations
{
    public class RegisterLocationHandler : TransactionalHandler<RegisterLocation>
    {

        public RegisterLocationHandler(
            IProvideTransactions transaction,
            IExecutionEngine publisher, 
            ILog log) :
            base(transaction, publisher, log)
        { 
        }

        protected override IEnumerable<DomainEvent> Handle(RegisterLocation cmd, IRepository repository)
        {
            var organization = repository.Load<Organization>(cmd.OrganizationId);

            if (organization == null)
            {
                throw new BusinessRuleException();
            }

            if (organization.Locations.Contains(cmd.Name))
            {
                throw new BusinessRuleException();
            }

            var location = new Location(cmd.LocationId, cmd.OrganizationId, cmd.Name, cmd.Coordinates);

            repository.Persist(location);

            return location.GetUncommittedChanges();
        }
    }
}
