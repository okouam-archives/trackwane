using System.Linq;
using Trackwane.AccessControl.Domain.Organizations;
using Trackwane.Framework.Interfaces;

namespace Trackwane.AccessControl.Engine.Services
{
    public class OrganizationService : IOrganizationService
    {
        public bool IsExistingOrganizationName(string applicationKey, string name, IRepository repository)
        {
            return repository.Query<Organization>().Any(x => x.Name == name && x.ApplicationKey == applicationKey);
        }

        public bool IsExistingOrganization(string applicationKey, string organizationKey, IRepository repository)
        {
           return repository.Query<Organization>().Any(x => x.Key == organizationKey && x.ApplicationKey == applicationKey); 
        }
    }
}
