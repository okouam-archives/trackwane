using System.Collections.Generic;
using System.Linq;
using Trackwane.AccessControl.Domain.Users;
using Trackwane.AccessControl.Engine.Commands.Users;
using Trackwane.Framework.Common;
using Trackwane.Framework.Common.Exceptions;
using Trackwane.Framework.Infrastructure.Requests;
using Trackwane.Framework.Interfaces;
using log4net;

namespace Trackwane.AccessControl.Engine.Processors.Handlers.Users
{
    public class UpdateUserHandler : Handler<UpdateUser>
    {
        public UpdateUserHandler(
            IProvideTransactions transaction,
            IExecutionEngine engine, 
            ILog log) : 
            base(engine, transaction, log)
        {
        }

        protected override IEnumerable<DomainEvent> Handle(UpdateUser cmd, IRepository repository)
        {
            var user = repository.Find<User>(cmd.UserKey, cmd.ApplicationKey);

            if (user == null)
            {
                throw new BusinessRuleException();
            }

            if (!string.IsNullOrEmpty(cmd.DisplayName))
            {
                user.UpdateDisplayName(cmd.DisplayName);
            }

            if (!string.IsNullOrEmpty(cmd.Email))
            {
                user.UpdateEmail(cmd.Email);
            }

            if (!string.IsNullOrEmpty(cmd.Password))
            {
                user.UpdatePassword(cmd.Password);
            }

            repository.Persist(user);

            return user.GetUncommittedChanges();
       }
    }
}
