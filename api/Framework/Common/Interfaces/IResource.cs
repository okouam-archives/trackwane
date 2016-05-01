namespace Trackwane.Framework.Common.Interfaces
{
    public interface IResource
    {
        string OrganizationKey { get; }
        
        void Archive();
    }
}