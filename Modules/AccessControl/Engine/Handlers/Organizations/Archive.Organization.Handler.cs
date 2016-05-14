using System.Collections.Generic;
using log4net;
using MassTransit;
using Trackwane.AccessControl.Domain.Organizations;
using Trackwane.AccessControl.Engine.Commands.Organizations;
using Trackwane.AccessControl.Engine.Services;
using Trackwane.Framework.Common;
using Trackwane.Framework.Common.Exceptions;
using Trackwane.Framework.Infrastructure.Requests;
using Trackwane.Framework.Interfaces;

namespace Trackwane.AccessControl.Engine.Processors.Handlers.Organizations
{
    public class ArchiveOrganizationHandler : Handler<ArchiveOrganization>
    {
        public ArchiveOrganizationHandler(IExecutionEngine engine, IProvideTransactions transaction, ILog log) : base(engine, transaction, log)
        {
        }

        protected override IEnumerable<DomainEvent> Handle(ArchiveOrganization cmd, IRepository repository)
        {
            var organization = repository.Find<Organization>(cmd.OrganizationKey, cmd.ApplicationKey);

            if (organization == null)
            {
                throw new NotFoundException(PhraseBook.Generate(Message.UNKNOWN_ORGANIZATION, cmd.OrganizationKey));
            }

            organization.Archive();

            repository.Persist(organization);

            return organization.GetUncommittedChanges();
        }
    }
}
