using System.Collections.Generic;
using System.Linq;
using Trackwane.Framework.Common;
using Trackwane.Framework.Common.Exceptions;
using Trackwane.Framework.Infrastructure.Requests;
using Trackwane.Framework.Interfaces;
using Trackwane.Management.Domain;
using Trackwane.Management.Engine.Commands.Drivers;
using Trackwane.Management.Engine.Services;
using log4net;

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
            var organization = repository.Find<Organization>(cmd.OrganizationKey, cmd.ApplicationKey);

            if (organization == null)
            {
                throw new BusinessRuleException(PhraseBook.Generate(Message.UNKNOWN_ORGANIZATION, cmd.OrganizationKey));
            }

            if (organization.Drivers.Any(x => x == cmd.Name))
            {
                throw new BusinessRuleException(PhraseBook.Generate(Message.DUPLICATE_DRIVER_NAME, cmd.Name));
            }

            var driver = new Driver(cmd.ApplicationKey, cmd.DriverKey, cmd.OrganizationKey, cmd.Name);

            repository.Persist(driver);
            
            return driver.GetUncommittedChanges();
        }
    }
}
