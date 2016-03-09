using System.Collections.Generic;
using System.Linq;
using paramore.brighter.commandprocessor.Logging;
using Trackwane.Framework.Common;
using Trackwane.Framework.Common.Exceptions;
using Trackwane.Framework.Infrastructure.Requests;
using Trackwane.Framework.Interfaces;
using Trackwane.Management.Domain;
using Trackwane.Management.Engine.Commands.Drivers;
using Trackwane.Management.Engine.Services;

namespace Trackwane.Management.Engine.Handlers.Drivers
{
    public class RegisterDriverHandler : TransactionalHandler<RegisterDriver>
    {
        /* Public */

        public RegisterDriverHandler(
            IProvideTransactions transaction,
            IExecutionEngine publisher, 
            ILog log) :
            base(transaction, publisher, log)
        {
        }

        protected override IEnumerable<DomainEvent> Handle(RegisterDriver cmd, IRepository repository)
        {
            var organization = repository.Load<Organization>(cmd.OrganizationKey);

            if (organization == null)
            {
                throw new BusinessRuleException(PhraseBook.Generate(Message.UNKNOWN_ORGANIZATION, cmd.OrganizationKey));
            }

            if (organization.Drivers.Any(x => x == cmd.Name))
            {
                throw new BusinessRuleException(PhraseBook.Generate(Message.DUPLICATE_DRIVER_NAME, cmd.Name));
            }

            var driver = new Driver(cmd.DriverKey, cmd.OrganizationKey, cmd.Name);

            repository.Persist(driver);
            
            return driver.GetUncommittedChanges();
        }
    }
}
