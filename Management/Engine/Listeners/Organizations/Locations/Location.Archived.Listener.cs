﻿using System.Collections.Generic;
using System.Linq;
using paramore.brighter.commandprocessor.Logging;
using Trackwane.Framework.Common;
using Trackwane.Framework.Common.Exceptions;
using Trackwane.Framework.Events;
using Trackwane.Framework.Infrastructure.Requests;
using Trackwane.Framework.Interfaces;
using Trackwane.Management.Domain;
using Trackwane.Management.Events;
using Trackwane.Management.Services;

namespace Trackwane.Management.Listeners.Organizations.Locations
{
    public class LocationArchivedListener : TransactionalListener<LocationArchived>
    {
        public LocationArchivedListener(IProvideTransactions transaction, IExecutionEngine publisher, ILog log) : base(transaction, publisher, log)
        {
        }

        protected override IEnumerable<DomainEvent> Handle(LocationArchived evt, IRepository repository)
        {
            var location = repository.Load<Location>(evt.LocationKey);

            var organization = repository.Load<Organization>(location.OrganizationKey);

            if (organization == null)
            {
                throw new BusinessRuleException(PhraseBook.Generate(Message.UNKNOWN_ORGANIZATION, evt.OrganizationKey));
            }

            organization.Locations = organization.Locations.Where(x => x != location.Name).ToList();

            repository.Persist(organization);

            return organization.GetUncommittedChanges().Cast<DomainEvent>();
        }
    }
}
