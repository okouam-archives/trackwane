using System.Collections.Generic;
using System.Linq;

namespace Trackwane.AccessControl.Domain.Users
{
    public static class AccessRightExtensions
    {
        public static bool Contain(this IEnumerable<AccessRight> accessRights, string organizationId, AccessClaim claim)
        {
            return accessRights.Any(x => x.OrganizationId == organizationId && x.Claim == claim);
        }

        internal static List<AccessRight> Revoke(this List<AccessRight> accessRights, string organizationId, AccessClaim claim)
        {
            var permission = Find(accessRights, organizationId, claim);

            if (permission != null)
            {
                accessRights.Remove(permission);
            }

            return accessRights;
        }

        internal static List<AccessRight> Grant(this List<AccessRight> accessRights, string organizationId, AccessClaim claim)
        {
            var existingPermission = Find(accessRights, organizationId, claim);

            if (existingPermission == null)
            {
                var newPermission = new AccessRight(claim, organizationId);

                accessRights.Add(newPermission);
            }

            return accessRights;
        }

        private static AccessRight Find(IEnumerable<AccessRight> accessRights, string organizationId, AccessClaim claim)
        {
            return accessRights.SingleOrDefault(x => x.Claim == claim && x.OrganizationId == organizationId);
        }
    }
}
