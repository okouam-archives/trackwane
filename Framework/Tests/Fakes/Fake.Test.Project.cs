using Trackwane.Framework.Infrastructure.Requests;

namespace Trackwane.Framework.Tests.Fakes
{
    public class CheckFramework : UserCommand
    {
        public CheckFramework(string requesterId) : base(requesterId)
        {
        }
    }
}
