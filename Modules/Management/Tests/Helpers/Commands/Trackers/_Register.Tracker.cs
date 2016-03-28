using System;
using Trackwane.Framework.Common;
using Trackwane.Management.Contracts.Models;

namespace Trackwane.Management.Tests.Helpers
{
    internal partial class Scenario
    {
        protected class _Register_Tracker
        {
            public static void With(UserClaims claims, string trackerId, string trackerHardwareId, string model,
                string organizationKey)
            {
                Client.Use(claims).CreateTracker(organizationKey, new RegisterTrackerModel(trackerId, trackerHardwareId, model));
            }

            public static void With(UserClaims claims, string trackerId, string organizationId)
            {
                With(claims, trackerId, Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), organizationId);
            }
        }
    }
}
