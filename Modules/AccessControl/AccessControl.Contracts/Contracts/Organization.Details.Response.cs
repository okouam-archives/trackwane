using System.Collections.Generic;

namespace Trackwane.AccessControl.Contracts.Contracts
{
    public class OrganizationDetailsResponse
    {
        public OrganizationDetailsResponse()
        {
            Viewers = new List<UserSummaryResponse>();

            Managers = new List<UserSummaryResponse>();

            Administrators = new List<UserSummaryResponse>();
        }

        public string Key { get; set; }

        public string Name { get; set; }

        public bool IsArchived { get; set; }

        public List<UserSummaryResponse> Viewers { get; set; }

        public List<UserSummaryResponse> Managers { get; set; }

        public List<UserSummaryResponse> Administrators { get; set; }
    }
}