using System.Collections.Generic;
using System.Linq;
using Marten;
using Trackwane.AccessControl.Contracts.Contracts;
using Trackwane.AccessControl.Domain.Organizations;
using Trackwane.AccessControl.Domain.Users;
using Trackwane.Framework.Common.Interfaces;
using Trackwane.Framework.Infrastructure.Queries;

namespace Trackwane.AccessControl.Engine.Queries.Organizations
{
    public class Find : Query<List<OrganizationDetailsResponse>>, IApplicationQuery
    {
        public Find(IDocumentStore documentStore) : base(documentStore)
        {
        }

        public List<OrganizationDetailsResponse> Execute()
        {
            return Execute(repository =>
            {
                var organizations = new List<OrganizationDetailsResponse>();

                var allOrganizations = repository.Query<Organization>();

                foreach (var organization in allOrganizations)
                {
                    if (organization == null) return null;

                    var result = new OrganizationDetailsResponse
                    {
                        IsArchived = organization.IsArchived,
                        Key = organization.Key,
                        Name = organization.Name
                    };

                    foreach (var user in organization.GetViewers().Select(key => repository.Find<User>(key, ApplicationKey)))
                    {
                        Add(result.Viewers, user);
                    }

                    foreach (var user in organization.GetManagers().Select(key => repository.Find<User>(key, ApplicationKey)))
                    {
                        Add(result.Managers, user);
                    }

                    foreach (var user in organization.GetAdministrators().Select(key => repository.Find<User>(key, ApplicationKey)))
                    {
                        Add(result.Administrators, user);
                    }

                    organizations.Add(result);
                }

                return organizations;
            });
        }

        private static void Add(ICollection<UserSummaryResponse> summaries, User user)
        {
            summaries.Add(new UserSummaryResponse
            {
                Email = user.Email,
                DisplayName = user.DisplayName,
                Id = user.Key
            });
        }
    }
}
