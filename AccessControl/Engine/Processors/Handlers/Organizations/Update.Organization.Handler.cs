using System.Collections.Generic;
using System.Linq;
using paramore.brighter.commandprocessor.Logging;
using Trackwane.AccessControl.Domain.Organizations;
using Trackwane.AccessControl.Engine.Commands.Organizations;
using Trackwane.AccessControl.Engine.Services;
using Trackwane.Framework.Common;
using Trackwane.Framework.Common.Exceptions;
using Trackwane.Framework.Infrastructure.Requests;
using Trackwane.Framework.Interfaces;

namespace Trackwane.AccessControl.Engine.Processors.Handlers.Organizations
{
    public class UpdateOrganizationHandler : TransactionalHandler<UpdateOrganization>
    {
        private readonly IOrganizationService organizationService;

        public UpdateOrganizationHandler(
            IProvideTransactions transaction,
            IExecutionEngine publisher, 
            ILog log,
            IOrganizationService organizationService) : 
            base(transaction, publisher, log)
        {
            this.organizationService = organizationService;
        }

        protected override IEnumerable<DomainEvent> Handle(UpdateOrganization cmd, IRepository repository)
        {
            var organization = repository.Load<Organization>(cmd.OrganizationKey);

            if (organization == null)
            {
                throw new BusinessRuleException();
            }

            if (!string.IsNullOrEmpty(cmd.Name))
            {
                if (organizationService.IsExistingOrganizationName(cmd.Name, repository))
                {
                    throw new BusinessRuleException();
                }

                organization.Update(cmd.Name);

                return organization.GetUncommittedChanges();
            }

            return Enumerable.Empty<DomainEvent>();
        }
    }
}
