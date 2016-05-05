using System.Collections.Generic;
using Trackwane.AccessControl.Domain.Users;
using Trackwane.AccessControl.Engine.Commands.Users;
using Trackwane.Framework.Common;
using Trackwane.Framework.Common.Exceptions;
using Trackwane.Framework.Infrastructure.Requests;
using Trackwane.Framework.Interfaces;
using log4net;
using Message = Trackwane.AccessControl.Engine.Services.Message;

namespace Trackwane.AccessControl.Engine.Processors.Handlers.Users
{
    public class ArchiveUserHandler : TransactionalHandler<ArchiveUser>
    {
        public ArchiveUserHandler(
            IProvideTransactions transaction,
            IExecutionEngine publisher, ILog log) : 
            base(transaction, log)
        {
        }

        protected override IEnumerable<DomainEvent> Handle(ArchiveUser cmd, IRepository repository)
        {
            var user = repository.Find<User>(cmd.UserKey, cmd.ApplicationKey);

            if (user == null)
            {
                throw new BusinessRuleException(PhraseBook.Generate(Message.UNKNOWN_USER, cmd.UserKey));
            }

            user.Archive();

            repository.Persist(user);

            return user.GetUncommittedChanges();
        }
    }
}
