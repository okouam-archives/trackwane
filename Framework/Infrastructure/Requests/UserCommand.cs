namespace Trackwane.Framework.Infrastructure.Requests
{
    public abstract class UserCommand : ApplicationCommand
    {
        protected UserCommand(string applicationKey, string requesterId) : base(applicationKey)
        {
            RequesterId = requesterId;
        }
        
        public string RequesterId { get; protected set; }
    }
}
