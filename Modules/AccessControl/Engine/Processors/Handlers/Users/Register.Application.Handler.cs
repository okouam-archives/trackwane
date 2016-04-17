using System;
using System.Collections.Generic;
using System.Linq;
using paramore.brighter.commandprocessor.Logging;
using Trackwane.AccessControl.Domain.Organizations;
using Trackwane.AccessControl.Domain.Users;
using Trackwane.AccessControl.Engine.Commands.Users;
using Trackwane.AccessControl.Engine.Services;
using Trackwane.Framework.Common;
using Trackwane.Framework.Common.Exceptions;
using Trackwane.Framework.Infrastructure.Requests;
using Trackwane.Framework.Interfaces;
using Role = Trackwane.AccessControl.Domain.Users.Role;

namespace Trackwane.AccessControl.Engine.Processors.Handlers.Users
{
    public class CreateApplicationHandler : TransactionalHandler<RegisterApplication>
    {
        public CreateApplicationHandler(
            IProvideTransactions transaction,
            IExecutionEngine publisher, 
            ILog log) : 
            base(transaction, publisher, log)
        {
        }

        protected override IEnumerable<DomainEvent> Handle(RegisterApplication cmd, IRepository repository)
        {
            if (repository.Query<User>().Any())
            {
                throw new BusinessRuleException(PhraseBook.Generate(Message.ROOT_ATTEMPT_WHEN_EXISTING_USERS));
            }

            var organization = new Organization(cmd.ApplicationKey, Guid.NewGuid().ToString(), "--INTERNAL--");

            repository.Persist(organization);

            var user = new User(cmd.ApplicationKey, organization.Key, cmd.UserKey, cmd.DisplayName, cmd.Email, Role.SystemManager, cmd.Password);

            repository.Persist(user);

            return user.GetUncommittedChanges();
        }
    }
}
