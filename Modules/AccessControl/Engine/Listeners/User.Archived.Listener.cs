using System.Collections.Generic;
using System.Linq;
using log4net;
using MassTransit;
using Trackwane.AccessControl.Contracts.Events;
using Trackwane.AccessControl.Domain.Organizations;
using Trackwane.Framework.Common;
using Trackwane.Framework.Infrastructure.Requests;
using Trackwane.Framework.Interfaces;

namespace Trackwane.AccessControl.Engine.Listeners
{
    public class UserArchivedListener : TransactionalListener<UserArchived>, IConsumer<UserArchived>
    {
        public UserArchivedListener(IProvideTransactions transaction, IExecutionEngine publisher, ILog log) : base(transaction, publisher, log)
        {
        }

        protected override IEnumerable<DomainEvent> Handle(UserArchived cmd, IRepository repository)
        {
            foreach (var organization in repository.Query<Organization>())
            {
                organization.RevokeViewPermission(cmd.UserKey);
                organization.RevokeManagePermission(cmd.UserKey);
                organization.RevokeAdministratePermission(cmd.UserKey);
            }

            return Enumerable.Empty<DomainEvent>();
        }
    }
}
