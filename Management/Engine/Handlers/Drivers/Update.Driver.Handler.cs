using System.Collections.Generic;
using System.Linq;
using paramore.brighter.commandprocessor.Logging;
using Trackwane.Framework.Common;
using Trackwane.Framework.Common.Exceptions;
using Trackwane.Framework.Events;
using Trackwane.Framework.Infrastructure.Requests;
using Trackwane.Framework.Interfaces;
using Trackwane.Management.Commands.Drivers;
using Trackwane.Management.Domain;
using Trackwane.Management.Services;

namespace Trackwane.Management.Handlers.Drivers
{
    public class UpdateDriverHandler : TransactionalHandler<UpdateDriver>
    {
        /* Public */

        public UpdateDriverHandler(
            IProvideTransactions transaction,
            IExecutionEngine publisher, ILog log) : 
            base(transaction, publisher, log)
        {
        }

        protected override IEnumerable<DomainEvent> Handle(UpdateDriver cmd, IRepository repository)
        {
            var driver = repository.Load<Driver>(cmd.DriverId);

            var organization = repository.Load<Organization>(cmd.OrganizationId);

            if (!string.IsNullOrEmpty(cmd.Name))
            {
                if (organization == null)
                {
                    throw new BusinessRuleException(PhraseBook.Generate(Message.UNKNOWN_ORGANIZATION, cmd.OrganizationId));
                }

                if (organization.Drivers.Contains(cmd.Name))
                {
                    throw new BusinessRuleException(PhraseBook.Generate(Message.DUPLICATE_DRIVER_NAME, cmd.Name));
                }
                
                driver.UpdateName(cmd.Name);
            }

            return driver.GetUncommittedChanges();
        }
    }
}
