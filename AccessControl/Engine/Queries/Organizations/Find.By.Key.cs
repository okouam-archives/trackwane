using System.Collections.Generic;
using System.Linq;
using Raven.Client;
using Trackwane.AccessControl.Domain.Organizations;
using Trackwane.AccessControl.Domain.Users;
using Trackwane.AccessControl.Models.Oganizations;
using Trackwane.AccessControl.Models.Users;
using Trackwane.Framework.Common.Interfaces;
using Trackwane.Framework.Infrastructure.Queries;

namespace Trackwane.AccessControl.Engine.Queries.Organizations
{
    public class FindByKey : Query<OrganizationDetails>, IScopedQuery
    {
        public FindByKey(IDocumentStore documentStore) : base(documentStore)
        {
        }

        public OrganizationDetails Execute()
        {
            return Execute(repository =>
            {
                var organization = repository.Load<Organization>(OrganizationKey);

                if (organization == null) return null;

                var result = new OrganizationDetails
                {
                    IsArchived = organization.IsArchived,
                    Key = OrganizationKey,
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

                return result;
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

        public string OrganizationKey { get; set; }
    }
}
