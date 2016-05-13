using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Marten;
using Trackwane.AccessControl.Contracts.Contracts;
using Trackwane.AccessControl.Domain.Organizations;
using Trackwane.AccessControl.Domain.Users;
using Trackwane.Framework.Common.Exceptions;
using Trackwane.Framework.Common.Interfaces;
using Trackwane.Framework.Infrastructure.Queries;
using Trackwane.Framework.Interfaces;

namespace Trackwane.AccessControl.Engine.Queries.Users
{
    public class FindByKey : Query<UserDetailsResponse>, IApplicationQuery
    {
        public FindByKey(IDocumentStore documentStore) : base(documentStore)
        {
        }

        public UserDetailsResponse Execute(string userId)
        {
            return Execute(repository =>
            {
                var user = repository.Find<User>(userId, ApplicationKey);

                if (user == null) throw new NotFoundException();

                var details = new UserDetailsResponse
                {
                    DisplayName = user.DisplayName,

                    Email = user.Email,

                    ParentOrganizationKey = user.ParentOrganizationKey,

                    Key = user.Key,

                    IsArchived = user.IsArchived,

                    Role = user.Role.ToString()
                };

                var availableOrganizations = GetAvailableOrganizations(repository);

                var canView = availableOrganizations.Where(x => x.Viewers.Contains(user.Key)).ToList();
                
                details.View = canView.Any() ? IncludeOrganizations(canView) : new List<KeyValuePair<string, string>>();
                
                var canManage = availableOrganizations.Where(x => x.Managers.Contains(user.Key)).ToList();

                details.Manage = canManage.Any() ? IncludeOrganizations(canManage) : new List<KeyValuePair<string, string>>();

                var canAdministrate = availableOrganizations.Where(x => x.Administrators.Contains(user.Key)).ToList();

                details.Administrate = canAdministrate.Any() ? IncludeOrganizations(canAdministrate) : new List<KeyValuePair<string, string>>();

                return details;
            });
        }

        private static List<KeyValuePair<string, string>> IncludeOrganizations(IEnumerable<Organization> list)
        {
            return list.Select(x => new KeyValuePair<string, string>(x.Key, x.Name)).ToList();
        }

        private List<Organization> GetAvailableOrganizations(IRepository repository)
        {
            return repository.Query<Organization>().Where(x => x.ApplicationKey == ApplicationKey).ToList();
        }
    }
}
