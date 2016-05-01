using Trackwane.AccessControl.Contracts.Models;
using Trackwane.Contracts;

namespace Trackwane.AccessControl.Contracts.Scopes
{
    public class ApplicationScope
    {
        private readonly TrackwaneClient ctx;

        public ApplicationScope(TrackwaneClient ctx)
        {
            this.ctx = ctx;
        }

        public void Register(RegisterApplicationModel model)
        {
            ctx.POST<RegisterApplicationModel>(ctx.Expand("/application"), model);
        }
    }
}