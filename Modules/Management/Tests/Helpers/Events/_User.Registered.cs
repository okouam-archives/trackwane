using Trackwane.AccessControl.Contracts.Events;

namespace Trackwane.Management.Tests.Helpers
{
    internal partial class Scenario
    {
        protected class _User_Registered
        {
            public static void With(string applicationKey, string userId)
            {
                Setup.EngineHost.ExecutionEngine.Handle(new UserRegistered
                {
                    ApplicationKey = applicationKey,
                    UserKey = userId
                });
            }
        }
    }
}
