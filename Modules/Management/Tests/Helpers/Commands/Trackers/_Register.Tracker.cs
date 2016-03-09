using System;
using Trackwane.Framework.Common;
using Trackwane.Management.Models.Trackers;

namespace Trackwane.Management.Tests.Helpers
{
    internal partial class Scenario
    {
        protected class _Register_Tracker
        {
            public static void With(UserClaims claims, string trackerId, string trackerHardwareId, string model,
                string organizationKey)
            {
                Client.Use(claims).Trackers.Create(organizationKey, new RegisterTrackerModel
                {
                    Model = model,
                    Key = trackerId,
                    HardwareId = trackerHardwareId
                });
            }

            public static void With(UserClaims claims, string trackerId, string organizationId)
            {
                With(claims, trackerId, Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), organizationId);
            }
        }
    }
}
