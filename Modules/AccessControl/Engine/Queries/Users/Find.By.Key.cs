using System.Collections.Generic;
using System.Linq;
using Marten;
using Trackwane.AccessControl.Contracts.Models;
using Trackwane.AccessControl.Domain.Organizations;
using Trackwane.AccessControl.Domain.Users;
using Trackwane.Framework.Common.Interfaces;
using Trackwane.Framework.Infrastructure.Queries;
using Trackwane.Framework.Interfaces;

namespace Trackwane.AccessControl.Engine.Queries.Users
{
    public class FindByKey : Query<UserDetails>, IApplicationQuery
    {
        public FindByKey(IDocumentStore documentStore) : base(documentStore)
        {
        }

        public UserDetails Execute(string userId)
        {
            return Execute(repository =>
            {
                var user = repository.Find<User>(userId, ApplicationKey);

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
                
                details.View = canView.Any() ? IncludeOrganizations(canView) : new List<KeyValuePair<string, string>>();
                
                var canManage = availableOrganizations.Where(x => x.GetManagers().Contains(user.Key)).ToList();

                details.Manage = canManage.Any() ? IncludeOrganizations(canManage) : new List<KeyValuePair<string, string>>();

                var canAdministrate = availableOrganizations.Where(x => x.GetAdministrators().Contains(user.Key)).ToList();

                details.Administrate = canAdministrate.Any() ? IncludeOrganizations(canAdministrate) : new List<KeyValuePair<string, string>>();

                return details;
            });
        }

        private static List<KeyValuePair<string, string>> IncludeOrganizations(IEnumerable<Organization> list)
        {
            return list.Select(x => new KeyValuePair<string, string>(x.Key, x.Name)).ToList();
        }

        private static List<Organization> GetAvailableOrganizations(IRepository repository)
        {
            return repository.Query<Organization>().ToList();
        }
    }
}
