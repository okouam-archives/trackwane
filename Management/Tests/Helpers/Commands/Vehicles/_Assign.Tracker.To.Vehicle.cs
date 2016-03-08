using System;
using Trackwane.Framework.Common;
using Trackwane.Management.Commands.Vehicles;

namespace Trackwane.Management.Tests.Helpers
{
    internal partial class Scenario
    {
        protected class _Assign_Tracker_To_Vehicle
        {
            public static void With(UserClaims claims, string organizationId, string vehicleId, string trackerId)
            {
                Client.Use(claims).Vehicles.AssignTrackerToVehicle(organizationId, vehicleId, trackerId);
            }
        }
    }
}
