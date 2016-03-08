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
using Message = Trackwane.Management.Services.Message;

namespace Trackwane.Management.Handlers.Locations
{
    public class UpdateLocationHandler : TransactionalHandler<UpdateLocation>
    {
        public UpdateLocationHandler(
            IProvideTransactions transaction,
            IExecutionEngine publisher, ILog log) : 
            base(transaction, publisher, log)
        {
        }

        protected override IEnumerable<DomainEvent> Handle(UpdateLocation cmd, IRepository repository)
        {
            var location = repository.Load<Location>(cmd.LocationId);

            if (!string.IsNullOrEmpty(cmd.Name))
            {
                var organization = repository.Load<Organization>(cmd.OrganizationId);

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
