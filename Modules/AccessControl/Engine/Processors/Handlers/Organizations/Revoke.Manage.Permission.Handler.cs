using System.Collections.Generic;
using paramore.brighter.commandprocessor.Logging;
using Trackwane.AccessControl.Domain.Organizations;
using Trackwane.AccessControl.Domain.Users;
using Trackwane.AccessControl.Engine.Commands.Organizations;
using Trackwane.AccessControl.Engine.Services;
using Trackwane.Framework.Common;
using Trackwane.Framework.Common.Exceptions;
using Trackwane.Framework.Infrastructure.Requests;
using Trackwane.Framework.Interfaces;

namespace Trackwane.AccessControl.Engine.Processors.Handlers.Organizations
{
    public class RevokeManagePermissionHandler : TransactionalHandler<RevokeManagePermission>
    {
        public RevokeManagePermissionHandler(
            IProvideTransactions transaction,
            IExecutionEngine publisher, ILog log) : 
            base(transaction, publisher, log)
        {
        }

        protected override IEnumerable<DomainEvent> Handle(RevokeManagePermission cmd, IRepository repository)
        {
            var organization = repository.Load<Organization>(cmd.OrganizationKey);

            if (organization == null)
            {
                throw new BusinessRuleException(PhraseBook.Generate(Message.UNKNOWN_ORGANIZATION, cmd.OrganizationKey));
            }

            var user = repository.Load<User>(cmd.UserKey);

            if (user == null)
            {
                throw new BusinessRuleException(PhraseBook.Generate(Message.UNKNOWN_USER, cmd.UserKey));
            }

            organization.RevokeManagePermission(user.Key);

            return organization.GetUncommittedChanges();
        }
    }
}
