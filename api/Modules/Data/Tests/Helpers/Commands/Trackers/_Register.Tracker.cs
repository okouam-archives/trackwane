using System;
using Trackwane.Framework.Common;
using Trackwane.Management.Contracts.Models;

namespace Trackwane.Tests
{
    internal partial class Scenario
    {
        protected class _Register_Tracker
        {
            public static void With(UserClaims claims, string trackerId, string trackerHardwareId, string model,
                string organizationKey)
            {
                Client.Use(claims).Trackers.Register(organizationKey, new RegisterTrackerModel {Key = trackerId, HardwareId = trackerHardwareId, Model = model});
            }

            public static void With(UserClaims claims, string trackerId, string organizationId)
            {
                With(claims, trackerId, Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), organizationId);
            }
        }
    }
}
