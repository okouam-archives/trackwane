namespace Trackwane.Framework.Common.Interfaces
{
    public interface IOrganizationQuery : IApplicationQuery
    {
        string OrganizationKey { set; }
    }
}