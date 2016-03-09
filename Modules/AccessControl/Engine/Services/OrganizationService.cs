using System.Linq;
using Trackwane.AccessControl.Domain.Organizations;
using Trackwane.Framework.Interfaces;

namespace Trackwane.AccessControl.Engine.Services
{
    public class OrganizationService : IOrganizationService
    {
        public bool IsExistingOrganizationName(string name, IRepository repository)
        {
            return repository.Query<Organization>()
             .Customize(x => x.WaitForNonStaleResults())
             .Any(x => x.Name == name);
        }

        public bool IsExistingOrganization(string organizationKey, IRepository repository)
        {
            var existingOrganization = repository.Load<Organization>(organizationKey);

            return existingOrganization != null;
        }
    }
}
