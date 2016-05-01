using System.Collections.Generic;
using System.Linq;
using Marten;
using Trackwane.AccessControl.Contracts.Models;
using Trackwane.AccessControl.Domain.Organizations;
using Trackwane.AccessControl.Domain.Users;
using Trackwane.Framework.Common.Interfaces;
using Trackwane.Framework.Infrastructure.Queries;

namespace Trackwane.AccessControl.Engine.Queries.Organizations
{
    public class FindByKey : Query<OrganizationDetails>, IOrganizationQuery
    {
        public FindByKey(IDocumentStore documentStore) : base(documentStore)
        {
        }

        public OrganizationDetails Execute()
        {
            return Execute(repository =>
            {
                var organization = repository.Find<Organization>(OrganizationKey, ApplicationKey);

                if (organization == null) return null;

                var result = new OrganizationDetails
                {
                    IsArchived = organization.IsArchived,
                    Key = OrganizationKey,
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
