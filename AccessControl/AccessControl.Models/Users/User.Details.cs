using System;
using System.Collections.Generic;

namespace Trackwane.AccessControl.Models.Users
{
    public class UserDetails
    {
        public string Key { get; set; }

        public string DisplayName { get; set; }

        public bool IsArchived { get; set; }

        public string Email { get; set; }

        public string Role { get; set; }

        public IList<Tuple<string, string>> View { get; set; } = new List<Tuple<string, string>>();

        public IList<Tuple<string, string>> Manage { get; set; } = new List<Tuple<string, string>>();

        public IList<Tuple<string, string>> Administrate { get; set; } = new List<Tuple<string, string>>();

        public string ParentOrganizationKey { get; set; }
    }
}
