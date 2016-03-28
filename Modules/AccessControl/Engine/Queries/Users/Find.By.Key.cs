using System;
using System.Collections.Generic;
using System.Linq;
using Raven.Client;
using Trackwane.AccessControl.Domain.Organizations;
using Trackwane.AccessControl.Domain.Users;
using Trackwane.AccessControl.Models;
using Trackwane.Framework.Common.Interfaces;
using Trackwane.Framework.Infrastructure.Queries;
using Trackwane.Framework.Interfaces;

namespace Trackwane.AccessControl.Engine.Queries.Users
{
    public class FindByKey : Query<UserDetails>, IUnscopedQuery
    {
        public FindByKey(IDocumentStore documentStore) : base(documentStore)
        {
        }

        public UserDetails Execute(string userId)
        {
            return Execute(repository =>
            {
                var user = repository.Load<User>(userId);

                if (user == null) return null;

                var details = new UserDetails
                {
                    DisplayName = user.DisplayName,

                    Email = user.Email,

                    ParentOrganizationKey = user.ParentOrganizationKey,

                    Key = user.Key,

                    IsArchived = user.IsArchived,

                    Role = user.Role.ToString()
                };

                var availableOrganizations = GetAvailableOrganizations(repository);

                var canView = availableOrganizations.Where(x => x.GetViewers().Contains(user.Key)).ToList();

                if (canView.Any())
                {
                    details.View = IncludeOrganizations(canView);
                }
                
                var canManage = availableOrganizations.Where(x => x.GetManagers().Contains(user.Key)).ToList();

                if (canManage.Any())
                {
                    details.Manage = IncludeOrganizations(canManage);
                }

                var canAdministrate = availableOrganizations.Where(x => x.GetAdministrators().Contains(user.Key)).ToList();

                if (canAdministrate.Any())
                {
                    details.Administrate = IncludeOrganizations(canAdministrate);
                }

                return details;
            });
        }

        private static List<Tuple<string, string>> IncludeOrganizations(IEnumerable<Organization> list)
        {
            return list.Select(x => new Tuple<string, string>(x.Key, x.Name)).ToList();
        }

        private static List<Organization> GetAvailableOrganizations(IRepository repository)
        {
            return repository.Query<Organization>().Customize(x => x.WaitForNonStaleResults()).ToList();
        }
    }
}
