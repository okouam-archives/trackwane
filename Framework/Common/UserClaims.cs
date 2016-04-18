using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using JWT;

namespace Trackwane.Framework.Common
{
    public class UserClaims
    {
        public string UserId { get; set; }

        public string ApplicationKey { get; set; }

        public bool IsSystemManager { get; set; }

        public List<string> View { get; set; } = new List<string>();

        public List<string> Manage { get; set; } = new List<string>();

        public List<string> Administrate { get; set; } = new List<string>();

        public string ParentOrganizationKey { get; set; }

        public IEnumerable<Claim> Claims
        {
            get
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, UserId),
                    new Claim(nameof(IsSystemManager), IsSystemManager.ToString())
                };

                if (ApplicationKey != null) claims.Add(new Claim(nameof(ApplicationKey), ApplicationKey));
                if (ParentOrganizationKey != null) claims.Add(new Claim(nameof(ParentOrganizationKey), ParentOrganizationKey));
                if (View != null) claims.Add(new Claim(nameof(View), string.Join(",", View)));
                if (Manage != null) claims.Add(new Claim(nameof(Manage), string.Join(",", Manage)));
                if (Administrate != null) claims.Add(new Claim(nameof(Administrate), string.Join(",", Administrate)));

                return claims;
            }
        }


        public UserClaims()
        {
        }

        public UserClaims(IEnumerable<Claim> claims)
        {
            var userClaims = claims.ToList();

            if (userClaims.All(x => x.Type != ClaimTypes.Name))
            {
                throw new Exception($"No claim of type <{ClaimTypes.Name}> was provided");
            }

            UserId = userClaims.Single(x => x.Type == ClaimTypes.Name).Value;

            View = userClaims.SingleOrDefault(x => x.Type == nameof(View))?.Value.Split(',').ToList();

            Manage = userClaims.SingleOrDefault(x => x.Type == nameof(Manage))?.Value.Split(',').ToList();

            Administrate = userClaims.SingleOrDefault(x => x.Type == nameof(Administrate))?.Value.Split(',').ToList();

            ApplicationKey = userClaims.SingleOrDefault(x => x.Type == nameof(ApplicationKey))?.Value;

            ParentOrganizationKey = userClaims.SingleOrDefault(x => x.Type == nameof(ParentOrganizationKey))?.Value;

            IsSystemManager = userClaims.Any(x => x.Type == nameof(IsSystemManager)) && Boolean.Parse(userClaims.Single(x => x.Type == nameof(IsSystemManager)).Value);
        }

        public UserClaims(string secretKey, string token)
        {
            var payload = JsonWebToken.DecodeToObject(token, secretKey) as IDictionary<string, object>;

            UserId = payload["subject"].ToString();

            ParentOrganizationKey = payload["parent_organization"]?.ToString();

            ApplicationKey = payload["application"]?.ToString();

            Manage = ExtractOrganizationList(payload, "manage");

            View = ExtractOrganizationList(payload, "view");

            if (payload["is_system_manager"] != null)
            {
                IsSystemManager = bool.Parse(payload["is_system_manager"].ToString());
            }
           
            Administrate = ExtractOrganizationList(payload, "administrate");
        }
        
        public string GenerateToken(string secretKey)
        {
            var payload = new Dictionary<string, object>
            {
                { "iss", "Trackwane" },
                { "subject", UserId },
                { "application", ApplicationKey },
                { "parent_organization", ParentOrganizationKey },
                { "is_system_manager", IsSystemManager },
                { "view", GenerateOrganizationList(View)},
                { "manage",  GenerateOrganizationList(Manage) },
                { "administrate", GenerateOrganizationList(Administrate) }
            };

            return JsonWebToken.Encode(payload, secretKey, JwtHashAlgorithm.HS256);
        }

        public bool CanView(string organizationKey)
        {
            return CanAdministrate(organizationKey) || CanManage(organizationKey) || (View != null && View.Contains(organizationKey));
        }

        public bool CanAdministrate(string organizationKey)
        {
            return (Administrate != null && Administrate.Contains(organizationKey)) || IsSystemManager;
        }

        public bool CanManage(string organizationKey)
        {
            return CanAdministrate(organizationKey) || (Manage != null && Manage.Contains(organizationKey));
        }

        /* Private */

        private static string GenerateOrganizationList(IReadOnlyCollection<string> values)
        {
            return values != null ? String.Join(",", values) : null;
        }

        private static List<string> ExtractOrganizationList(IDictionary<string, object> payload, string property)
        {
            return payload[property] != null ? new List<string>(payload[property].ToString().Split(',')) : null;
        }
    }
}
