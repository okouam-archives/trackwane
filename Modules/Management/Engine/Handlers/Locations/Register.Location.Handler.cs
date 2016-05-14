using System.Collections.Generic;
using log4net;
using Trackwane.Framework.Common;
using Trackwane.Framework.Common.Exceptions;
using Trackwane.Framework.Infrastructure.Requests;
using Trackwane.Framework.Interfaces;
using Trackwane.Management.Domain;
using Trackwane.Management.Engine.Commands.Locations;

namespace Trackwane.Management.Engine.Handlers.Locations
{
    public class RegisterLocationHandler : Handler<RegisterLocation>
    {

        public RegisterLocationHandler(
            IProvideTransactions transaction,
            IExecutionEngine publisher, 
            ILog log) :
            base(publisher, transaction, log)
        {  
        }

        protected override IEnumerable<DomainEvent> Handle(RegisterLocation cmd, IRepository repository)
        {
            var organization = repository.Find<Organization>(cmd.OrganizationId, cmd.ApplicationKey);

            if (organization == null)
            {
                throw new BusinessRuleException("The organization could not be found");
            }

            if (organization.Locations.Contains(cmd.Name))
            {
                throw new BusinessRuleException("The organization already contains a location with the same name");
            }

            var location = new Location(cmd.ApplicationKey, cmd.LocationId, cmd.OrganizationId, cmd.Name, cmd.Coordinates);

            repository.Persist(location);

            return location.GetUncommittedChanges();
        }
    }
}
