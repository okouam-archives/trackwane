using System.Collections.Generic;
using Trackwane.AccessControl.Domain.Users;
using Trackwane.AccessControl.Engine.Commands.Users;
using Trackwane.AccessControl.Engine.Services;
using Trackwane.Framework.Common;
using Trackwane.Framework.Common.Exceptions;
using Trackwane.Framework.Infrastructure.Requests;
using Trackwane.Framework.Interfaces;
using Message = Trackwane.AccessControl.Engine.Services.Message;
using Role = Trackwane.AccessControl.Domain.Users.Role;
using log4net;

namespace Trackwane.AccessControl.Engine.Processors.Handlers.Users
{
    public class RegisterUserHandler : TransactionalHandler<RegisterUser>
    {
        private readonly IUserService userService;

        public RegisterUserHandler(
            IProvideTransactions transaction,
            IExecutionEngine publisher, 
            ILog log, 
            IUserService userService) : 
            base(transaction, log)
        {
            this.userService = userService;
        }

        protected override IEnumerable<DomainEvent> Handle(RegisterUser cmd, IRepository repository)
        {
            if (userService.IsExistingUser(cmd.ApplicationKey, cmd.UserKey, repository))
            {
                throw new BusinessRuleException();
            }

            if (userService.IsExistingEmail(cmd.ApplicationKey, cmd.Email, repository))
            {
                throw new BusinessRuleException(PhraseBook.Generate(Message.DUPLICATE_USER_EMAIL, cmd.Email));
            }

            var user = new User(cmd.ApplicationKey, cmd.OrganizationKey, cmd.UserKey, cmd.DisplayName, cmd.Email, (Role)(int)cmd.Role, cmd.Password);

            repository.Persist(user);

            return user.GetUncommittedChanges();
        }
    }
}
