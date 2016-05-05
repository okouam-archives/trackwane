using System.Collections.Generic;

namespace Trackwane.AccessControl.Contracts.Contracts
{
    public class UserDetailsResponse
    {
        public UserDetailsResponse()
        {
            View = new List<KeyValuePair<string, string>>(); 
            Manage = new List<KeyValuePair<string, string>>();
            Administrate = new List<KeyValuePair<string, string>>();
        }

        public string Key { get; set; }

        public string DisplayName { get; set; }

        public bool IsArchived { get; set; }

        public string Email { get; set; }

        public string Role { get; set; }

        public List<KeyValuePair<string, string>> View { get; set; }

        public List<KeyValuePair<string, string>> Manage { get; set; }

        public List<KeyValuePair<string, string>> Administrate { get; set; }

        public string ParentOrganizationKey { get; set; }
    }
}