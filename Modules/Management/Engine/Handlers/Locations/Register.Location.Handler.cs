using System.Collections.Generic;
using paramore.brighter.commandprocessor.Logging;
using Trackwane.Framework.Common;
using Trackwane.Framework.Common.Exceptions;
using Trackwane.Framework.Infrastructure.Requests;
using Trackwane.Framework.Interfaces;
using Trackwane.Management.Domain;
using Trackwane.Management.Engine.Commands.Locations;

namespace Trackwane.Management.Engine.Handlers.Locations
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
            var organization = repository.Find<Organization>(cmd.OrganizationId, cmd.ApplicationKey);

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
