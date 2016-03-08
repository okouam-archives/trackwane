﻿using System.Collections.Generic;
using paramore.brighter.commandprocessor.Logging;
using Trackwane.AccessControl.Domain.Organizations;
using Trackwane.AccessControl.Domain.Users;
using Trackwane.AccessControl.Engine.Commands.Organizations;
using Trackwane.Framework.Common;
using Trackwane.Framework.Common.Exceptions;
using Trackwane.Framework.Infrastructure.Requests;
using Trackwane.Framework.Interfaces;

namespace Trackwane.AccessControl.Engine.Processors.Handlers.Organizations
{
    public class RevokeReadAccessHandler : TransactionalHandler<RevokeViewPermission>
    {
        public RevokeReadAccessHandler(
            IProvideTransactions transaction,
            IExecutionEngine publisher,
            ILog log) : 
            base(transaction, publisher, log)
        {
        }

        protected override IEnumerable<DomainEvent> Handle(RevokeViewPermission cmd, IRepository repository)
        {
            var organization = repository.Load<Organization>(cmd.OrganizationKey);

            if (organization == null)
            {
                throw new BusinessRuleException();
            }

            var user = repository.Load<User>(cmd.UserKey);

            if (user == null)
            {
                throw new BusinessRuleException();
            }

            organization.RevokeViewPermission(user.Key);

            return organization.GetUncommittedChanges();
        }
    }
}
