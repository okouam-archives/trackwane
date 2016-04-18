namespace Trackwane.Framework.Infrastructure.Requests
{
    public abstract class ApplicationCommand : SystemCommand
    {
        public string ApplicationKey { get; set; }

        protected ApplicationCommand(string applicationKey)
        {
            ApplicationKey = applicationKey;
        }
    }
}