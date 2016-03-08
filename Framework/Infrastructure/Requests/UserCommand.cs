namespace Trackwane.Framework.Infrastructure.Requests
{
    public abstract class UserCommand : SystemCommand
    {
        protected UserCommand(string requesterId)
        {
            RequesterId = requesterId;
        }
        
        public string RequesterId { get; protected set; }
    }
}
