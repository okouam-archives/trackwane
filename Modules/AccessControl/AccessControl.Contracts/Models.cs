using System.Collections.Generic;

namespace Trackwane.AccessControl.Contracts.Models
{
    public class UserDetails
    {
        public UserDetails()
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

    public class UserSummary
    {
        public string Id { get; set; }

        public string DisplayName { get; set; }

        public string Email { get; set; }
    }
    
    public class RegisterApplicationModel
    {
        public RegisterApplicationModel(string userKey, string email, string displayName, string password, string secretKey)
        {
            UserKey = userKey;
            Email = email;
            DisplayName = displayName;
            Password = password;
            SecretKey = secretKey;
        }

        public RegisterApplicationModel()
        {
        }

        public string SecretKey { get; set; }

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
        public OrganizationDetails()
        {
            Viewers = new List<UserSummary>();

            Managers = new List<UserSummary>();

            Administrators = new List<UserSummary>();
        }

        public string Key { get; set; }

        public string Name { get; set; }

        public bool IsArchived { get; set; }

        public List<UserSummary> Viewers { get; set; }

        public List<UserSummary> Managers { get; set; }

        public List<UserSummary> Administrators { get; set; }
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
