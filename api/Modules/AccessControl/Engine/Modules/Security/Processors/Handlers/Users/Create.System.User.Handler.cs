using System.Collections.Generic;
using paramore.brighter.commandprocessor.Logging;
using Trackwane.AccessControl.Commands.Users;
using Trackwane.Framework.Common;
using Trackwane.Framework.Events;
using Trackwane.Framework.Infrastructure.Requests;

namespace Trackwane.AccessControl.Handlers.Users
{
    public class CreateSystemUserHandler : TransactionalHandler<CreateSystemUser>
    {
        public CreateSystemUserHandler(IProvideTransactions transaction, IPublishEvents publisher, ILog log) : base(transaction, publisher, log)
        {
        }

        protected override IEnumerable<Event> Handle(CreateSystemUser command, IRepository repository)
        {
            throw new System.NotImplementedException();
        }
    }
}
