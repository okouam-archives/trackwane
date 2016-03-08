using System.Collections.Generic;
using Trackwane.AccessControl.Models.Users;

namespace Trackwane.AccessControl.Models.Oganizations
{
    public class OrganizationDetails
    {
        public string Key { get; set; }

        public string Name { get; set; }

        public bool IsArchived { get; set; }

        public IList<UserSummary> Viewers { get; set; } = new List<UserSummary>();
        
        public IList<UserSummary> Managers { get; set; } = new List<UserSummary>();

        public IList<UserSummary> Administrators { get; set; } = new List<UserSummary>();
    }
}
