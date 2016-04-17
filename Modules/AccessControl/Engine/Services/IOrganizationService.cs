using Trackwane.Framework.Interfaces;

namespace Trackwane.AccessControl.Engine.Services
{
    public interface IOrganizationService
    {
        bool IsExistingOrganizationName(string applicationKey, string name, IRepository repository);

        bool IsExistingOrganization(string applicationKey, string organizationKey, IRepository repository);
    }
}