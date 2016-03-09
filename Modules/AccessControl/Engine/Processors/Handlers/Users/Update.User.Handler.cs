using System.Collections.Generic;
using System.Linq;
using paramore.brighter.commandprocessor.Logging;
using Trackwane.AccessControl.Domain.Users;
using Trackwane.AccessControl.Engine.Commands.Users;
using Trackwane.Framework.Common;
using Trackwane.Framework.Common.Exceptions;
using Trackwane.Framework.Infrastructure.Requests;
using Trackwane.Framework.Interfaces;

namespace Trackwane.AccessControl.Engine.Processors.Handlers.Users
{
    public class UpdateUserHandler : TransactionalHandler<UpdateUser>
    {
        public UpdateUserHandler(
            IProvideTransactions transaction,
            IExecutionEngine publisher, 
            ILog log) : 
            base(transaction, publisher, log)
        {
        }

        protected override IEnumerable<DomainEvent> Handle(UpdateUser cmd, IRepository repository)
        {
            var user = repository.Load<User>(cmd.UserKey);

            if (user == null)
            {
                throw new BusinessRuleException();
            }

            if (!string.IsNullOrEmpty(cmd.DisplayName))
            {
                user.Update(cmd.DisplayName);

                return user.GetUncommittedChanges();
            }

            return Enumerable.Empty<DomainEvent>();
        }
    }
}
