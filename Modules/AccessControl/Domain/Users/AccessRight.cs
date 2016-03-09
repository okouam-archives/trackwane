namespace Trackwane.AccessControl.Domain.Users
{
    public class AccessRight
    {
        public AccessRight(AccessClaim claim, string organizationId)
        {
            Claim = claim;
            OrganizationId = organizationId;
        }

        public string OrganizationId { get; set; }

        public AccessClaim Claim { get; set; }
    }
}
