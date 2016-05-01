using Trackwane.AccessControl.Contracts.Events;

namespace Trackwane.Tests
{
    internal partial class Scenario
    {
        protected class _User_Registered
        {
            public static void With(string applicationKey, string userId)
            {
                Setup.EngineHost.ExecutionEngine.Publish(new UserRegistered
                {
                    ApplicationKey = applicationKey,
                    UserKey = userId
                });
            }
        }
    }
}
