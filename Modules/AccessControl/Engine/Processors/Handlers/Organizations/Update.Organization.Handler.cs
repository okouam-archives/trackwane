using System.Collections.Generic;
using System.Linq;
using log4net;
using Trackwane.AccessControl.Domain.Organizations;
using Trackwane.AccessControl.Engine.Commands.Organizations;
using Trackwane.AccessControl.Engine.Services;
using Trackwane.Framework.Common;
using Trackwane.Framework.Common.Exceptions;
using Trackwane.Framework.Infrastructure.Requests;
using Trackwane.Framework.Interfaces;

namespace Trackwane.AccessControl.Engine.Processors.Handlers.Organizations
{
    public class UpdateOrganizationHandler : Handler<UpdateOrganization>
    {
        private readonly IOrganizationService organizationService;

        public UpdateOrganizationHandler(
            IProvideTransactions transaction,
            IExecutionEngine publisher, 
            ILog log,
            IOrganizationService organizationService) : 
            base(publisher, transaction, log)
        {
            this.organizationService = organizationService;
        }

        protected override IEnumerable<DomainEvent> Handle(UpdateOrganization cmd, IRepository repository)
        {
            var organization = repository.Find<Organization>(cmd.OrganizationKey, cmd.ApplicationKey);

            if (organization == null)
            {
                throw new BusinessRuleException();
            }

            if (!string.IsNullOrEmpty(cmd.Name))
            {
                if (organizationService.IsExistingOrganizationName(cmd.ApplicationKey, cmd.Name, repository))
                {
                    throw new BusinessRuleException();
                }

                organization.Update(cmd.Name);

                repository.Persist(organization);

                return organization.GetUncommittedChanges();
            }

            return Enumerable.Empty<DomainEvent>();
        }
    }
}
