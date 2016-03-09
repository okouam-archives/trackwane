using Trackwane.Framework.Interfaces;

namespace Trackwane.AccessControl.Engine.Services
{
    public interface IOrganizationService
    {
        bool IsExistingOrganizationName(string name, IRepository repository);

        bool IsExistingOrganization(string organizationKey, IRepository repository);
    }
}