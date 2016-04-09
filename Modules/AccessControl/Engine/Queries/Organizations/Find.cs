using System.Collections.Generic;
using System.Linq;
using Raven.Client;
using Trackwane.AccessControl.Contracts.Models;
using Trackwane.AccessControl.Domain.Organizations;
using Trackwane.AccessControl.Domain.Users;
using Trackwane.Framework.Common.Interfaces;
using Trackwane.Framework.Infrastructure.Queries;

namespace Trackwane.AccessControl.Engine.Queries.Organizations
{
    public class Find : Query<List<OrganizationDetails>>, IUnscopedQuery
    {
        public Find(IDocumentStore documentStore) : base(documentStore)
        {
        }

        public List<OrganizationDetails> Execute()
        {
            return Execute(repository =>
            {
                var organizations = new List<OrganizationDetails>();

                var allOrganizations = repository.Query<Organization>();

                foreach (var organization in allOrganizations)
                {
                    if (organization == null) return null;

                    var result = new OrganizationDetails
                    {
                        IsArchived = organization.IsArchived,
                        Key = organization.Key,
                        Name = organization.Name
                    };

                    foreach (var user in organization.GetViewers().Select(repository.Load<User>))
                    {
                        Add(result.Viewers, user);
                    }

                    foreach (var user in organization.GetManagers().Select(repository.Load<User>))
                    {
                        Add(result.Managers, user);
                    }

                    foreach (var user in organization.GetAdministrators().Select(repository.Load<User>))
                    {
                        Add(result.Administrators, user);
                    }

                    organizations.Add(result);
                }

                return organizations;
            });
        }

        private static void Add(ICollection<UserSummary> summaries, User user)
        {
            summaries.Add(new UserSummary
            {
                Email = user.Email,
                DisplayName = user.DisplayName,
                Id = user.Key
            });
        }
    }
}
