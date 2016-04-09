using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trackwane.AccessControl.Contracts.Models
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

    public class UserSummary
    {
        public string Id { get; set; }

        public string DisplayName { get; set; }

        public string Email { get; set; }
    }

    public class RegisterUserModel
    {
        public RegisterUserModel(string userKey, string email, string displayName, string password)
        {
            UserKey = userKey;
            Email = email;
            DisplayName = displayName;
            Password = password;
        }

        public RegisterUserModel()
        {
        }

        public string Password { get; set; }

        public string Email { get; set; }

        public string DisplayName { get; set; }

        public string UserKey { get; set; }
    }

    public class UpdateUserModel
    {
        public string Password { get; set; }

        public string Email { get; set; }

        public string DisplayName { get; set; }
    }

    public class OrganizationDetails
    {
        public string Key { get; set; }

        public string Name { get; set; }

        public bool IsArchived { get; set; }

        public IList<UserSummary> Viewers { get; set; } = new List<UserSummary>();

        public IList<UserSummary> Managers { get; set; } = new List<UserSummary>();

        public IList<UserSummary> Administrators { get; set; } = new List<UserSummary>();
    }

    public class RegisterOrganizationModel
    {
        public string Name { get; set; }

        public string OrganizationKey { get; set; }
    }

    public class UpdateOrganizationModel
    {
        public string Name { get; set; }
    }
}
