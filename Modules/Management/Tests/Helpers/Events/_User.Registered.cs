using Trackwane.AccessControl.Contracts.Events;

namespace Trackwane.Management.Tests.Helpers
{
    internal partial class Scenario
    {
        protected class _User_Registered
        {
            public static void With(string userId)
            {
                EngineHost.ExecutionEngine.Publish(new UserRegistered
                {
                    UserKey = userId
                });
            }
        }
    }
}
