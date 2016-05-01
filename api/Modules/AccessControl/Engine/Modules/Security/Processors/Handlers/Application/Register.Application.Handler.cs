using System;
using System.Collections.Generic;
using System.Linq;
using paramore.brighter.commandprocessor.Logging;
using Trackwane.AccessControl.Domain.Organizations;
using Trackwane.AccessControl.Domain.Users;
using Trackwane.AccessControl.Engine.Commands.Application;
using Trackwane.AccessControl.Engine.Services;
using Trackwane.Framework.Common;
using Trackwane.Framework.Common.Exceptions;
using Trackwane.Framework.Infrastructure.Requests;
using Trackwane.Framework.Interfaces;
using Role = Trackwane.AccessControl.Domain.Users.Role;

namespace Trackwane.AccessControl.Engine.Processors.Handlers.Application
{
    public class RegisterApplicationHandler : TransactionalHandler<RegisterApplication>
    {
        public RegisterApplicationHandler(
            IProvideTransactions transaction,
            IExecutionEngine publisher, 
            ILog log) : 
            base(transaction, publisher, log)
        {
        }

        protected override IEnumerable<DomainEvent> Handle(RegisterApplication cmd, IRepository repository)
        {
            var applicationKeyInUse = repository.Query<User>().Any(x => x.ApplicationKey == cmd.ApplicationKey);

            if (applicationKeyInUse)
            {
                throw new Exception("The application key {XXX} is already in use");
            }

            var user = new User(cmd.ApplicationKey, null, cmd.UserKey, cmd.DisplayName, cmd.Email, Role.SystemManager, cmd.Password);

            repository.Persist(user);

            return user.GetUncommittedChanges();
        }
    }
}
