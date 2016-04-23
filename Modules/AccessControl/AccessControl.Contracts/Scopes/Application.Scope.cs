using Trackwane.AccessControl.Contracts.Models;

namespace Trackwane.AccessControl.Contracts.Scopes
{
    public class ApplicationScope
    {
        private readonly AccessControlClient ctx;

        public ApplicationScope(AccessControlClient ctx)
        {
            this.ctx = ctx;
        }

        public void Register(RegisterApplicationModel model)
        {
            ctx.POST<RegisterApplicationModel>(ctx.Expand("/application"), model);
        }
    }
}