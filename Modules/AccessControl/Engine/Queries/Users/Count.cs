﻿using System.Linq;
using Raven.Client;
using Trackwane.AccessControl.Domain.Users;
using Trackwane.Framework.Common.Interfaces;
using Trackwane.Framework.Infrastructure.Queries;

namespace Trackwane.AccessControl.Engine.Queries.Users
{
    public class Count : Query<int>, IUnscopedQuery
    {
        public Count(IDocumentStore documentStore) : base(documentStore)
        {
        }

        public int Execute(string organizationKey = null)
        {
            return Execute(repository =>
            {
                var query = repository.Query<User>();
                return organizationKey == null ? query.Count() : query.Count(x => x.ParentOrganizationKey == organizationKey);
            });
        }
    }
}
