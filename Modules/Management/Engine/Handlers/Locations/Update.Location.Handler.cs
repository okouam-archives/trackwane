using System.Collections.Generic;
using Trackwane.Framework.Common;
using Trackwane.Framework.Common.Exceptions;
using Trackwane.Framework.Infrastructure.Requests;
using Trackwane.Framework.Interfaces;
using Trackwane.Management.Domain;
using Trackwane.Management.Engine.Commands.Locations;
using Message = Trackwane.Management.Engine.Services.Message;
using log4net;

namespace Trackwane.Management.Engine.Handlers.Locations
{
    public class UpdateLocationHandler : TransactionalHandler<UpdateLocation>
    {
        public UpdateLocationHandler(
            IProvideTransactions transaction,
            IExecutionEngine publisher, ILog log) : 
            base(transaction, log)
        {
        }

        protected override IEnumerable<DomainEvent> Handle(UpdateLocation cmd, IRepository repository)
        {
            var location = repository.Find<Location>(cmd.LocationId, cmd.ApplicationKey);

            if (!string.IsNullOrEmpty(cmd.Name))
            {
                var organization = repository.Find<Organization>(cmd.OrganizationId, cmd.ApplicationKey);

                if (organization == null)
                {
                    throw new BusinessRuleException(PhraseBook.Generate(Message.UNKNOWN_ORGANIZATION, cmd.OrganizationId));
                }

                if (organization.Locations.Contains(cmd.Name))
                {
                    throw new BusinessRuleException(PhraseBook.Generate(Message.DUPLICATION_LOCATION_NAME, cmd.Name));
                }

                location.ChangeName(cmd.Name);
            }

            if (cmd.Coordinates == null)
            {
                location.ChangeCoordinates(cmd.Coordinates);
            }

            return location.GetUncommittedChanges();
        }
    }
}
